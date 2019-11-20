using Ateliex.Cadastro.Modelos;
using Ateliex.Decisoes.Comerciais;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliex
{
    public static class WindowsServiceCollectionExtensions
    {
        public static IServiceCollection AddWindows(this IServiceCollection services)
        {
            services.AddTransient(typeof(MainWindow));

            services.AddTransient(typeof(ModelosWindow));

            services.AddTransient(typeof(ConsultaDeModelosWindow));

            services.AddTransient(typeof(PlanosComerciaisWindow));

            return services;
        }
    }
}
