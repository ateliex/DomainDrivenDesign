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
            services.AddDbContext<EventStoreDbContext>(options =>
                options.UseSqlServer(@"Data Source=EventStore.db"), ServiceLifetime.Transient);

            //

            services.AddTransient<IAppendOnlyStore>(factory => new SqlClientStore(@"Data Source=EventStore.db"));

            //

            return services;
        }

        public static void EnsureDatabaseCreatedAsync(IServiceScopeFactory serviceScopeFactory)
        {
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var eventStoreDbContext = serviceScope.ServiceProvider.GetService<EventStoreDbContext>();

                //eventStoreDbContext.Database.EnsureDeleted();

                eventStoreDbContext.Database.EnsureCreated();

                eventStoreDbContext.Database.Migrate();
            }
        }
    }
}
