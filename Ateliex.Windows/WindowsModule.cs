using Ateliex.Cadastro.Modelos;
using Ateliex.Decisoes.Comerciais;
using Microsoft.Extensions.DependencyInjection;

namespace Ateliex
{
    public static class WindowsModule
    {
        public static IServiceCollection AddWindows(this IServiceCollection services)
        {
            services.AddTransient<MainWindow>();

            //

            services.AddTransient<EventStoreViewModelCollection>();

            services.AddTransient<EventStoreWindow>();

            //

            services.AddTransient<ModelosViewModel>();

            services.AddTransient<ModelosWindow>();

            services.AddTransient<ConsultaDeModelosWindow>();

            services.AddTransient<PlanosComerciaisObservableCollection>();

            services.AddTransient<PlanosComerciaisWindow>();

            return services;
        }
    }
}
