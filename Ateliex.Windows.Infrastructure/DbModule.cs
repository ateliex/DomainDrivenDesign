using Microsoft.Extensions.DependencyInjection;
using System.DomainModel;

namespace Ateliex
{
    public static class DbModule
    {
        internal static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            services.AddTransient<IAppendOnlyStore>(factory => new SqliteStore(@"Data Source=Ateliex.db"));

            return services;
        }
    }
}
