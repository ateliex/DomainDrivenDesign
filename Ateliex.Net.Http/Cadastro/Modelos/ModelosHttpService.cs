using Ateliex.Cadastro.Modelos.CadastroDeModelos;
using Ateliex.Cadastro.Modelos.ConsultaDeModelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosHttpService : IConsultaDeModelos, ICadastroDeModelos
    {
        public ModelosHttpService()
        {

        }

        public Task<RespostaDeConsultaDeModelos> ConsultaModelos(ParametrosDeConsultaDeModelos parametros)
        {
            throw new NotImplementedException();
        }

        public Task<RespostaDeAdicaoDeRecursoDeModelo> AdicionaRecursoDeModelo(SolicitacaoDeAdicaoDeRecursoDeModelo solicitacao)
        {
            throw new NotImplementedException();
        }

        public Task<RespostaDeCadastroDeModelo> CadastraModelo(SolicitacaoDeCadastroDeModelo solicitacao)
        {
            var resposta = new RespostaDeCadastroDeModelo
            {

            };

            return Task.FromResult(resposta);
        }

        public Task RemoveModelo(string codigo)
        {
            return Task.CompletedTask;
        }

        public Task RemoveRecursoDeModelo(string codigo, string descricao)
        {
            return Task.CompletedTask;
        }
    }
}
