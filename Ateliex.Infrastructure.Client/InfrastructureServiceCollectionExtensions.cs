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
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AteliexDbContext>(options =>
                options.UseSqlite(@"Data Source=Ateliex.db"));

            services.AddTransient(typeof(IUnitOfWork), typeof(TransactionScopeManager));

            //

            services.AddTransient<ModelosObservableCollection>();

            services.AddTransient<IConsultaDeModelos, ModelosInfraService>();

            services.AddTransient<IRepositorioDeModelos, ModelosInfraService>();

            services.AddTransient<ModelosDbService>();

            services.AddTransient<ModelosHttpService>();

            //

            services.AddTransient<PlanosComerciaisObservableCollection>();

            services.AddTransient<IConsultaDePlanosComerciais, PlanosComerciaisInfraService>();

            services.AddTransient<IRepositorioDePlanosComerciais, PlanosComerciaisInfraService>();

            services.AddTransient<PlanosComerciaisDbService>();

            return services;
        }
    }
}
