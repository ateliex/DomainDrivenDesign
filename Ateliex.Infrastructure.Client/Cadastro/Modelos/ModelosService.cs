using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosService : IConsultaDeModelos, IRepositorioDeModelos
    {
        private readonly ModelosDbService db;

        public ModelosService(ModelosDbService db)
        {
            this.db = db;
        }

        public async Task<RespostaDeConsultaDeModelos> ConsultaModelos(ParametrosDeConsultaDeModelos parametros)
        {
            var resposta = await db.ConsultaModelos(parametros);

            return resposta;
        }

        public async Task Add(Modelo modelo)
        {
            await db.Add(modelo);
        }

        public async Task Update(Modelo modelo)
        {
            await db.Update(modelo);
        }

        public async Task Remove(Modelo modelo)
        {
            await db.Remove(modelo);
        }

        public async Task<Modelo> ObtemModelo(string id)
        {
            var modelo = await db.ObtemModelo(id);

            return modelo;
        }

        public async Task<IEnumerable<Modelo>> ObtemObservavelDeModelos()
        {
            var modelos = await db.ObtemObservavelDeModelos();

            return modelos;
        }
    }
}
