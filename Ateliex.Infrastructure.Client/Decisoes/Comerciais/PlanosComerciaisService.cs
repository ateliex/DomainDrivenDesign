using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Ateliex.Decisoes.Comerciais
{
    public class PlanosComerciaisService : IConsultaDePlanosComerciais, IRepositorioDePlanosComerciais
    {
        private readonly PlanosComerciaisDbService db;

        public PlanosComerciaisService(PlanosComerciaisDbService db)
        {
            this.db = db;
        }

        public async Task<RespostaDeConsultaDePlanosComerciais> ConsultaPlanosComerciais(ParametrosDeConsultaDePlanosComerciais parametros)
        {
            var resposta = await db.ConsultaPlanosComerciais(parametros);

            return resposta;
        }

        public async Task Add(PlanoComercial planoComercial)
        {
            await db.Add(planoComercial);
        }

        public async Task Update(PlanoComercial planoComercial)
        {
            await db.Update(planoComercial);
        }

        public async Task Remove(PlanoComercial planoComercial)
        {
            await db.Remove(planoComercial);
        }

        public async Task<PlanoComercial> ObtemPlanoComercial(string id)
        {
            var planoComercial = await db.ObtemPlanoComercial(id);

            return planoComercial;
        }

        public async Task<IEnumerable<PlanoComercial>> ObtemObservavelDePlanosComerciais()
        {
            var planosComerciais = await db.ObtemObservavelDePlanosComerciais();

            return planosComerciais;
        }
    }
}
