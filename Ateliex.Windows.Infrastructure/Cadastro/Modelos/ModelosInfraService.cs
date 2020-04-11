using Ateliex.Cadastro.Modelos.ConsultaDeModelos;
using System;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosInfraService : IConsultaDeModelos, IRepositorioDeModelos
    {
        private readonly ModelosDbService db;

        //private readonly ModelosHttpService http;

        public ModelosInfraService(ModelosDbService db)
        {
            this.db = db;

            //this.http = http;
        }

        public IObservable<Modelo[]> ConsultaModelos(ParametrosDeConsultaDeModelos parametros)
        {
            var modelos = db.ConsultaModelos(parametros);

            return modelos;
        }

        public async Task<Modelo> ObtemModelo(CodigoDeModelo codigo)
        {
            var modelo = await db.ObtemModelo(codigo);

            return modelo;
        }

        public async Task Add(Modelo modelo)
        {
            await db.Add(modelo);

            //http.Add(modelo);
        }

        public async Task Update(Modelo modelo)
        {
            await db.Update(modelo);

            //http.Update(solicitacao);

            // TODO.
        }

        public async Task Remove(Modelo modelo)
        {
            await db.Remove(modelo);

            //http.Remove(modelo);
        }
    }
}
