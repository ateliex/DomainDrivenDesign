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
            services.AddTransient<MainWindow>();

            services.AddTransient<ModelosViewModel>();

            services.AddTransient<ModelosWindow>();

            services.AddTransient<ConsultaDeModelosWindow>();

            services.AddTransient<PlanosComerciaisObservableCollection>();

            services.AddTransient<PlanosComerciaisWindow>();

            return services;
        }
    }
}
