using System;
using System.Collections.Generic;
using System.DomainModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class Modelo : Entity
    {
        public CodigoDeModelo Id { get; set; }

        public string Codigo { get; set; }

        public string Nome { get; set; }

        public decimal CustoDeProducao
        {
            get
            {
                var total = Recursos.Sum(p => p.CustoPorUnidade);

                return total;
            }
        }

        public virtual ICollection<Recurso> Recursos { get; set; }

        public Modelo(string codigo, string nome)
        {
            Id = new CodigoDeModelo(codigo);

            Codigo = codigo;

            Nome = nome;

            Recursos = new HashSet<Recurso>();
        }

        public void AlteraCodigo(CodigoDeModelo codigo)
        {
            Apply(new CodigoDeModeloAlterado(codigo));
        }

        public void AlteraNome(string nome)
        {
            Apply(new NomeDeModeloAlterado(nome));
        }

        public void When(CodigoDeModeloAlterado e)
        {
            Id = e.Codigo;
        }

        public void When(NomeDeModeloAlterado e)
        {
            Nome = e.Nome;
        }

        public Recurso AdicionaRecurso(TipoDeRecurso tipo, string descricao, decimal custo, int quantidade)
        {
            var recurso = new Recurso(this, tipo, descricao, custo, quantidade);

            Recursos.Add(recurso);

            return recurso;
        }

        public void When(DescricaoDeRecursoDeModeloAlterado e)
        {
            var recurso = Recursos.FirstOrDefault(p => p.Descricao == e.DescricaoAntiga);

            //recurso.When(e);
        }

        public void RemoveRecurso(Recurso recurso)
        {
            Recursos.Remove(recurso);
        }

        public Modelo()
        {

        }

        public Modelo(IEnumerable<IEvent> events)
        {
            Recursos = new HashSet<Recurso>();

            foreach (var @event in events)
            {
                Mutate(@event);
            }
        }
    }

    public class CodigoDeModelo : IIdentity
    {
        private readonly string valor;

        public CodigoDeModelo(string valor)
        {
            this.valor = valor;
        }

        public override string ToString()
        {
            return $"Modelo-{valor}";
        }
    }

    [Serializable]
    public class CodigoDeModeloAlterado : IEvent
    {
        public CodigoDeModelo Codigo { get; }

        public CodigoDeModeloAlterado(CodigoDeModelo codigo)
        {
            Codigo = codigo;
        }
    }

    [Serializable]
    public class NomeDeModeloAlterado : IEvent
    {        
        public string Nome { get; }

        public NomeDeModeloAlterado(string nome)
        {
            Nome = nome;
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
        public virtual Modelo Modelo { get; set; }

        public virtual TipoDeRecurso Tipo { get; set; }

        public virtual string Descricao { get; set; }

        public decimal Custo { get; set; }

        public int Unidades { get; set; }

        public decimal CustoPorUnidade
        {
            get
            {
                var custoPorUnidade = Custo / Unidades;

                return custoPorUnidade;
            }
        }

        public Recurso(Modelo modelo, TipoDeRecurso tipo, string descricao, decimal custo, int unidades)
        {
            Modelo = modelo;

            Tipo = tipo;

            Descricao = descricao;

            Custo = custo;

            Unidades = unidades;
        }

        public void DefineTipo(TipoDeRecurso tipo)
        {
            Tipo = tipo;
        }

        public void DefineDescricao(string descricao)
        {
            Modelo.Apply(new DescricaoDeRecursoDeModeloAlterado(descricao));
        }

        public void When(DescricaoDeRecursoDeModeloAlterado e)
        {
            Descricao = e.Descricao;
        }

        public void DefineCusto(decimal custo)
        {
            Custo = custo;
        }

        public void DefineUnidades(int unidades)
        {
            Unidades = unidades;
        }

        public Recurso()
        {

        }

        public string ModeloCodigo { get; set; }
    }

    [Serializable]
    public class DescricaoDeRecursoDeModeloAlterado : IEvent
    {
        public string DescricaoAntiga { get; }
        
        public string Descricao { get; }

        public DescricaoDeRecursoDeModeloAlterado(string descricao)
        {
            Descricao = descricao;
        }
    }

    public interface IRepositorioDeModelos
    {
        Task<Modelo[]> ObtemModelos();

        Task<Modelo> ObtemModelo(string id);

        Task Add(Modelo modelo);

        Task Update(Modelo modelo);

        Task Remove(Modelo modelo);
    }
}
