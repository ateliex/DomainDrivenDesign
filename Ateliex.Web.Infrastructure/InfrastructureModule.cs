using Microsoft.Extensions.DependencyInjection;
using System.DomainModel;
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

            services.AddDbServices();

            //

            return services;
        }
    }
}
