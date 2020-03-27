using Ateliex.Cadastro.Modelos;
using Ateliex.Cadastro.Modelos.ConsultaDeModelos;
using Ateliex.Decisoes.Comerciais;
using Ateliex.Decisoes.Comerciais.ConsultaDePlanosComerciais;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.DomainModel;
using System.Transactions;

namespace Ateliex
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AteliexDbContext>(options =>
                options.UseSqlite(@"Data Source=Ateliex.db"));

            services.AddTransient<IUnitOfWork, TransactionScopeManager>();

            services.AddTransient<IEventStore, EventStore>();

            services.AddTransient<IAppendOnlyStore>(factory => new SqliteStore(@"Data Source=Ateliex.db"));

            //

            services.AddTransient<IConsultaDeModelos, ModelosInfraService>();

            services.AddTransient<IRepositorioDeModelos, ModelosInfraService>();

            services.AddTransient<ModelosDbService>();

            services.AddTransient<ModelosHttpService>();

            //

            services.AddTransient<IConsultaDePlanosComerciais, PlanosComerciaisInfraService>();

            services.AddTransient<IRepositorioDePlanosComerciais, PlanosComerciaisInfraService>();

            services.AddTransient<PlanosComerciaisDbService>();

            return services;
        }
    }
}
