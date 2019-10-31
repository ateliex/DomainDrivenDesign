using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliex.Cadastro.Modelos
{
    public interface ICadastroDeModelos
    {
        RespostaDeCadastroDeModelo CadastraModelo(SolicitacaoDeCadastroDeModelo solicitacao);

        RespostaDeAdicaoDeRecursoDeModelo AdicionaRecursoDeModelo(SolicitacaoDeAdicaoDeRecursoDeModelo solicitacao);

        void RemoveRecursoDeModelo(string codigo, string descricao);

        void RemoveModelo(string codigo);
    }

    public class SolicitacaoDeCadastroDeModelo
    {
        public string Codigo { get; set; }

        public string Nome { get; set; }
    }

    public class RespostaDeCadastroDeModelo
    {

    }

    public class SolicitacaoDeAdicaoDeRecursoDeModelo
    {
        public string Codigo { get; set; }

        public virtual TipoDeRecurso Tipo { get; set; }

        public virtual string Descricao { get; set; }

        public decimal Custo { get; set; }

        public int Unidades { get; set; }
    }

    public class RespostaDeAdicaoDeRecursoDeModelo
    {

    }

    internal class CadastroDeModelos : ICadastroDeModelos
    {
        public CadastroDeModelos()
        {

        }

        public RespostaDeCadastroDeModelo CadastraModelo(SolicitacaoDeCadastroDeModelo solicitacao)
        {
            throw new NotImplementedException();
        }

        public RespostaDeAdicaoDeRecursoDeModelo AdicionaRecursoDeModelo(SolicitacaoDeAdicaoDeRecursoDeModelo solicitacao)
        {
            throw new NotImplementedException();
        }

        public void RemoveRecursoDeModelo(string codigo, string descricao)
        {
            throw new NotImplementedException();
        }

        public void RemoveModelo(string codigo)
        {
            throw new NotImplementedException();
        }
    }
}
