using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosInfraService : IConsultaDeModelos, IRepositorioDeModelos
    {
        private readonly ModelosDbService db;

        private readonly ModelosHttpService http;

        public ModelosInfraService(ModelosDbService db, ModelosHttpService http)
        {
            this.db = db;

            this.http = http;
        }

        public async Task<RespostaDeConsultaDeModelos> ConsultaModelos(ParametrosDeConsultaDeModelos parametros)
        {
            var resposta = await db.ConsultaModelos(parametros);

            return resposta;
        }

        public async Task Add(Modelo modelo)
        {
            await db.Add(modelo);

            var solicitacao = new SolicitacaoDeCadastroDeModelo
            {
                Nome = modelo.Nome
            };

            await http.CadastraModelo(solicitacao);
        }

        public async Task Update(Modelo modelo)
        {
            await db.Update(modelo);

            //await http.AdicionaRecursoDeModelo(solicitacao);

            // TODO.
        }

        public async Task Remove(Modelo modelo)
        {
            await db.Remove(modelo);

            await http.RemoveModelo(modelo.Codigo);
        }

        public async Task<Modelo> ObtemModelo(string id)
        {
            var modelo = await db.ObtemModelo(id);

            return modelo;
        }

        public async Task<Modelo[]> ObtemModelos()
        {
            var modelos = await db.ObtemModelos();

            return modelos;
        }
    }
}
