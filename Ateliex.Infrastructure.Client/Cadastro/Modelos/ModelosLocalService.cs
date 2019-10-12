using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosLocalService : IRepositorioDeModelos
    {
        private readonly ModelosDbService db;

    public ModelosLocalService(ModelosDbService db)
    {
        this.db = db;
    }

    public async Task SaveChanges()
    {
        await db.SaveChanges();
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
        var result = await db.ObtemModelo(id);

        return result;
    }

    public async Task<IEnumerable<Modelo>> ObtemObservavelDeModelos()
    {
        var result = await db.ObtemObservavelDeModelos();

        return result; //.Cast<Modelo[]>();
    }
}
}
