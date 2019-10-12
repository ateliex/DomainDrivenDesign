using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosDbService : IRepositorioDeModelos
    {
        private readonly AtelieDbContext db;

        public ModelosDbService(AtelieDbContext db)
        {
            this.db = db;
        }

        public async Task SaveChanges()
        {
            var items = db.ChangeTracker.Entries<Recurso>().ToArray();

            foreach (var item in items)
            {
                item.State.ToString();
            }

            await db.SaveChangesAsync();
        }

        public async Task Add(Modelo modelo)
        {
            try
            {
                await db.Modelos.AddAsync(modelo);

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException($"Erro ao adicionar modelo '{modelo.Codigo}'.", ex);
            }
        }

        public async Task Update(Modelo modelo)
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException();
            }
        }

        public async Task Remove(Modelo modelo)
        {
            try
            {
                db.Modelos.Remove(modelo);

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException($"Erro ao excluir modelo '{modelo.Codigo}'.", ex);
            }
        }

        public async Task<Modelo> ObtemModelo(string id)
        {
            try
            {
                var modelo = await db.Modelos.FindAsync(id);

                return modelo;
            }
            catch (Exception ex)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException($"Erro ao obter modelo '{id}'.", ex);
            }
        }

        public async Task<IEnumerable<Modelo>> ObtemObservavelDeModelos()
        {
            try
            {
                var planosComerciais = await db.Modelos
                    .Include(p => p.Recursos)
                    .ToListAsync();

                //var observable = planosComerciais.ToObservable();

                return planosComerciais;
            }
            catch (Exception ex)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException("Erro em Planos Comerciais.", ex);
            }
        }
    }
}
