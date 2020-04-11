using Ateliex.Cadastro.Modelos.ConsultaDeModelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosDbService : IConsultaDeModelos, IRepositorioDeModelos
    {
        private readonly AteliexDbContext db;

        public ModelosDbService(AteliexDbContext db)
        {
            this.db = db;
        }

        public IObservable<Modelo[]> ConsultaModelos(ParametrosDeConsultaDeModelos parametros)
        {
            try
            {
                var modelos = db.Modelos
                    .Include(p => p.Codigo)
                    .Include(p => p.Recursos)
                    .ToArrayAsync();

                return modelos.ToObservable();
            }
            catch (Exception ex)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException("Erro ao consultar modelos.", ex);
            }
        }

        public async Task<Modelo> ObtemModelo(CodigoDeModelo codigo)
        {
            try
            {
                var modelo = await db.Modelos
                    .Include(p => p.Codigo)
                    .Include(p => p.Recursos)
                    .FirstOrDefaultAsync(p => p.Codigo.Valor == codigo.Valor);

                if (modelo == default(Modelo))
                {
                    throw new ApplicationException();
                }

                return modelo;
            }
            catch (Exception ex)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException($"Erro ao obter modelo '{codigo.Valor}'.", ex);
            }
        }

        public Modelo ObtemModeloParaEdicao(CodigoDeModelo codigo)
        {
            try
            {
                var modelo = db.Modelos.Find(codigo.Valor);

                return modelo;
            }
            catch (Exception ex)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException($"Erro ao obter modelo '{codigo.Valor}'.", ex);
            }
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
            catch (Exception ex)
            {
                // TODO: Tratar erros de persistência aqui.

                throw new ApplicationException($"Erro ao atualizar modelo '{modelo.Codigo}'.", ex);
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

        //public void SaveChanges()
        //{
        //    var items = db.ChangeTracker.Entries<Recurso>().ToArray();

        //    foreach (var item in items)
        //    {
        //        item.State.ToString();
        //    }

        //    db.SaveChanges();
        //}
    }
}
