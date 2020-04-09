using Ateliex.Cadastro.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.DomainModel;
using System.Threading.Tasks;

namespace Ateliex
{
    public static class DbModule
    {
        internal static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            services.AddDbContext<AteliexDbContext>(options =>
                options.UseSqlite(@"Data Source=Ateliex.db"), ServiceLifetime.Singleton);

            //

            services.AddDbContext<EventStoreDbContext>(options =>
                options.UseSqlite(@"Data Source=EventStore.db"), ServiceLifetime.Singleton);

            //

            services.AddTransient<IAppendOnlyStore>(factory => new SqliteStore(@"Data Source=EventStore.db"));

            //

            services.AddSingleton<ModelosDbService>();

            //

            return services;
        }

        public static void EnsureDatabaseCreatedAsync(IServiceScopeFactory serviceScopeFactory)
        {
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var ateliexDbContext = serviceScope.ServiceProvider.GetService<AteliexDbContext>();

                //ateliexDbContext.Database.EnsureDeleted();

                ateliexDbContext.Database.EnsureCreated();

                ateliexDbContext.Database.Migrate();

                //

                var eventStoreDbContext = serviceScope.ServiceProvider.GetService<EventStoreDbContext>();

                //eventStoreDbContext.Database.EnsureDeleted();

                eventStoreDbContext.Database.EnsureCreated();

                eventStoreDbContext.Database.Migrate();
            }
        }

        public static async Task LimpaBancoDeDadosDeLeitura(IServiceScopeFactory serviceScopeFactory)
        {
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var ateliexDbContext = serviceScope.ServiceProvider.GetService<AteliexDbContext>();

                await ateliexDbContext.Database.EnsureDeletedAsync();

                await ateliexDbContext.Database.EnsureCreatedAsync();

                //await ateliexDbContext.Database.MigrateAsync();
            }
        }
    }
}
