using Ateliex.Cadastro.Modelos;
using Ateliex.Decisoes.Comerciais;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Ateliex
{
    public static class HttpClientServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            services.AddDbContext<AteliexDbContext>(options =>
                options.UseSqlite(@"Data Source=Ateliex.db"));

            services.AddTransient(typeof(IUnitOfWork), typeof(TransactionScopeManager));

            services.AddTransient<ModelosInfraService>();

            services.AddTransient<ModelosDbService>();

            services.AddTransient<PlanosComerciaisInfraService>();

            services.AddTransient<PlanosComerciaisDbService>();

            return services;
        }
    }
}
