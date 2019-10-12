using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class Modelo
    {
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
            Codigo = codigo;

            Nome = nome;

            Recursos = new HashSet<Recurso>();
        }

        public void DefineCodigo(string codigo)
        {
            Codigo = codigo;
        }

        public void DefineNome(string nome)
        {
            Nome = nome;
        }

        public Recurso AdicionaRecurso(TipoDeRecurso tipo, string descricao, decimal custo, int quantidade)
        {
            var recurso = new Recurso(this, tipo, descricao, custo, quantidade);

            Recursos.Add(recurso);

            return recurso;
        }

        public void RemoveRecurso(Recurso recurso)
        {
            Recursos.Remove(recurso);
        }

        public Modelo()
        {
            Recursos = new HashSet<Recurso>();
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

        public Recurso(Modelo modelo, TipoDeRecurso tipo, string descricao, decimal custo, int quantiade)
        {
            Modelo = modelo;

            Tipo = tipo;

            Descricao = descricao;

            Custo = custo;

            Unidades = quantiade;
        }

        public void DefineTipo(TipoDeRecurso tipo)
        {
            Tipo = tipo;
        }

        public void DefineDescricao(string descricao)
        {
            Descricao = descricao;
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

    public interface IRepositorioDeModelos
    {
        Task<Modelo> ObtemModelo(string id);

        Task Add(Modelo modelo);

        Task Update(Modelo modelo);

        Task Remove(Modelo modelo);
    }
}
