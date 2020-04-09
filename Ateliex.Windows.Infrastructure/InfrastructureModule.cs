using Ateliex.Cadastro.Modelos;
using Ateliex.Cadastro.Modelos.ConsultaDeModelos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.DomainModel;
using System.Reflection;
using System.Transactions;

namespace Ateliex
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, TransactionScopeManager>();

            services.AddTransient<IEventStore, EventStore>();

            //

            services.AddTransient<IConsultaDeModelos, ModelosInfraService>();

            services.AddTransient<IRepositorioDeModelos, ModelosInfraService>();

            //

            //services.AddTransient<IConsultaDePlanosComerciais, PlanosComerciaisInfraService>();

            //services.AddTransient<IRepositorioDePlanosComerciais, PlanosComerciaisInfraService>();

            //

            services.AddDbServices();

            services.AddHttpServices();

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ModeloCriadoHandler).Assembly);

            //

            return services;
        }
    }
}
