using System;
using System.Collections.Generic;
using System.DomainModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class Modelo : AggregateRoot
    {
        public CodigoDeModelo Codigo { get; internal set; }

        public string Nome { get; internal set; }

        public decimal CustoDeProducao
        {
            get
            {
                var total = Recursos.Sum(p => p.CustoPorUnidade);

                return total;
            }
        }

        public virtual ICollection<Recurso> Recursos { get; internal set; }

        public Modelo(CodigoDeModelo codigo, string nome)
        {
            Apply(new ModeloCriado(codigo, nome));

            Recursos = new HashSet<Recurso>();
        }

        public void When(ModeloCriado e)
        {
            Codigo = e.Codigo;

            Nome = e.Nome;

            Id = Codigo.Valor;
        }

        public void AlteraCodigo(CodigoDeModelo codigo)
        {
            Apply(new CodigoDeModeloAlterado(codigo));
        }

        public void When(CodigoDeModeloAlterado e)
        {
            Codigo = e.Codigo;
        }

        public void AlteraNome(string nome)
        {
            Apply(new NomeDeModeloAlterado(Codigo, nome));
        }

        public void When(NomeDeModeloAlterado e)
        {
            Nome = e.Nome;
        }

        public void AdicionaRecurso(TipoDeRecurso tipo, string descricao, decimal custo, int quantidade)
        {
            Apply(new RecursoDeModeloAdicionado(Codigo, tipo, descricao, custo, quantidade));
        }

        public event Action<Recurso> RecursoAdicionado;

        public void When(RecursoDeModeloAdicionado e)
        {
            int nextId;

            if (Recursos.Any())
            {
                var maxId = Recursos.Max(recurso => recurso.Id);

                nextId = ++maxId;
            }
            else
            {
                nextId = 1;
            }

            var recurso = new Recurso(this, nextId, e.Tipo, e.Descricao, e.Custo, e.Quantidade);

            Recursos.Add(recurso);

            RecursoAdicionado?.Invoke(recurso);
        }

        public void AlteraDescricaoDeRecurso(int id, string descricao)
        {
            Apply(new DescricaoDeRecursoDeModeloAlterado(Codigo, id, descricao));
        }

        public void When(DescricaoDeRecursoDeModeloAlterado e)
        {
            var recurso = Recursos.First(p => p.Id == e.Id);

            recurso.When(e);
        }

        public void RemoveRecurso(Recurso recurso)
        {
            Recursos.Remove(recurso);
        }

        public void Exclui()
        {
            Apply(new ModeloExcluido(Codigo));
        }

        public void When(ModeloExcluido e)
        {

        }

        public Modelo()
        {
            Recursos = new HashSet<Recurso>();
        }

        public Modelo(IEnumerable<Event> events)
        {
            Recursos = new HashSet<Recurso>();

            foreach (var @event in events)
            {
                Mutate(@event);
            }
        }
    }

    [Serializable]
    public class CodigoDeModelo : IIdentity
    {
        public string Valor { get; internal set; }

        public CodigoDeModelo(string valor)
        {
            Valor = valor;
        }

        public override string ToString()
        {
            return $"Modelo-{Valor}";
        }

        internal CodigoDeModelo()
        {

        }
    }

    public enum TipoDeRecurso
    {
        Material,
        Transporte,
        Humano
    }

    public class Recurso
    {
        public virtual Modelo Modelo { get; internal set; }

        public int Id { get; internal set; }

        public virtual TipoDeRecurso Tipo { get; internal set; }

        public virtual string Descricao { get; internal set; }

        public decimal Custo { get; internal set; }

        public int Unidades { get; internal set; }

        public decimal CustoPorUnidade
        {
            get
            {
                var custoPorUnidade = Custo / Unidades;

                return custoPorUnidade;
            }
        }

        public Recurso(Modelo modelo, int id, TipoDeRecurso tipo, string descricao, decimal custo, int unidades)
        {
            Modelo = modelo;

            ModeloCodigo = modelo.Codigo.Valor;

            Id = id;

            Tipo = tipo;

            Descricao = descricao;

            Custo = custo;

            Unidades = unidades;
        }

        public void AlteraTipo(TipoDeRecurso tipo)
        {
            Tipo = tipo;
        }

        public void AlteraDescricao(string descricao)
        {
            Modelo.AlteraDescricaoDeRecurso(Id, descricao);
        }

        public void When(DescricaoDeRecursoDeModeloAlterado e)
        {
            Descricao = e.Descricao;
        }

        public void AlteraCusto(decimal custo)
        {
            Custo = custo;
        }

        public void AlteraUnidades(int unidades)
        {
            Unidades = unidades;
        }

        public Recurso()
        {

        }

        public string ModeloCodigo { get; internal set; }
    }

    [Serializable]
    public class ModeloCriado : Event
    {
        public CodigoDeModelo Codigo { get; }

        public string Nome { get; }

        public ModeloCriado(CodigoDeModelo codigo, string nome)
        {
            Codigo = codigo;

            Nome = nome;
        }
    }

    [Serializable]
    public class CodigoDeModeloAlterado : Event
    {
        public CodigoDeModelo Codigo { get; }

        public CodigoDeModeloAlterado(CodigoDeModelo codigo)
        {
            Codigo = codigo;
        }
    }

    [Serializable]
    public class NomeDeModeloAlterado : Event
    {
        public CodigoDeModelo Codigo { get; }

        public string Nome { get; }

        public NomeDeModeloAlterado(CodigoDeModelo codigo, string nome)
        {
            Codigo = codigo;

            Nome = nome;
        }
    }

    [Serializable]
    public class RecursoDeModeloAdicionado : Event
    {
        public CodigoDeModelo Codigo { get; }

        public TipoDeRecurso Tipo { get; }

        public string Descricao { get; }

        public decimal Custo { get; }

        public int Quantidade { get; }

        public RecursoDeModeloAdicionado(CodigoDeModelo codigo, TipoDeRecurso tipo, string descricao, decimal custo, int quantidade)
        {
            Codigo = codigo;

            Tipo = tipo;

            Descricao = descricao;

            Custo = custo;

            Quantidade = quantidade;
        }
    }

    [Serializable]
    public class DescricaoDeRecursoDeModeloAlterado : Event
    {
        public CodigoDeModelo Codigo { get; }

        public int Id { get; }

        public string Descricao { get; }

        public DescricaoDeRecursoDeModeloAlterado(CodigoDeModelo codigo, int id, string descricao)
        {
            Codigo = codigo;

            Id = id;

            Descricao = descricao;
        }
    }

    [Serializable]
    public class ModeloExcluido : Event
    {
        public CodigoDeModelo Codigo { get; }

        public ModeloExcluido(CodigoDeModelo codigo)
        {
            Codigo = codigo;
        }
    }

    public interface IRepositorioDeModelos
    {
        Task<Modelo> ObtemModelo(CodigoDeModelo codigo);

        Task Add(Modelo modelo);

        Task Update(Modelo modelo);

        Task Remove(Modelo modelo);
    }
}
