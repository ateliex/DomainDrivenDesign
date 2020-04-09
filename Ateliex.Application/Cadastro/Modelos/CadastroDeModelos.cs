using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos.CadastroDeModelos
{
    public interface ICadastroDeModelos
    {
        Task<RespostaDeCadastroDeModelo> CadastraModelo(SolicitacaoDeCadastroDeModelo solicitacao);

        Task<RespostaDeAdicaoDeRecursoDeModelo> AdicionaRecursoDeModelo(SolicitacaoDeAdicaoDeRecursoDeModelo solicitacao);

        Task RemoveRecursoDeModelo(string codigo, string descricao);

        Task RemoveModelo(string codigo);
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

    public class CadastroDeModelos : ICadastroDeModelos
    {
        public CadastroDeModelos()
        {

        }

        public Task<RespostaDeCadastroDeModelo> CadastraModelo(SolicitacaoDeCadastroDeModelo solicitacao)
        {
            throw new NotImplementedException();
        }

        public Task<RespostaDeAdicaoDeRecursoDeModelo> AdicionaRecursoDeModelo(SolicitacaoDeAdicaoDeRecursoDeModelo solicitacao)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRecursoDeModelo(string codigo, string descricao)
        {
            throw new NotImplementedException();
        }

        public Task RemoveModelo(string codigo)
        {
            throw new NotImplementedException();
        }
    }
}
