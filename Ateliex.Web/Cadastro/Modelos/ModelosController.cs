using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ateliex.Cadastro.Modelos
{
    [Route("[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly IConsultaDeModelos consultaDeModelos;

        private readonly ICadastroDeModelos cadastroDeModelos;

        public ModelosController(IConsultaDeModelos consultaDeModelos, ICadastroDeModelos cadastroDeModelos)
        {
            this.consultaDeModelos = consultaDeModelos;

            this.cadastroDeModelos = cadastroDeModelos;
        }

        // GET: api/Modelos
        [HttpGet]
        public RespostaDeConsultaDeModelos Get()
        {
            var parametros = new ParametrosDeConsultaDeModelos();

            var resposta = consultaDeModelos.ConsultaModelos(parametros);

            return resposta;
        }

        // GET: api/Modelos/5
        [HttpGet("{codigo}", Name = "Get")]
        public string Get(int codigo)
        {
            throw new NotImplementedException();
        }

        // POST: api/Modelos
        [HttpPost]
        public RespostaDeCadastroDeModelo Post(SolicitacaoDeCadastroDeModelo solicitacao)
        {
            var resposta = cadastroDeModelos.CadastraModelo(solicitacao);

            return resposta;
        }

        // PUT: api/Modelos/5
        [HttpPut("{codigo}")]
        public void Put(int codigo, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{codigo}")]
        public void Delete(string codigo)
        {
            cadastroDeModelos.RemoveModelo(codigo);
        }
    }
}
