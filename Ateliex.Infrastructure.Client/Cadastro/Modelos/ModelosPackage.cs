
using Microsoft.Extensions.DependencyInjection;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ModelosService>();

            services.AddTransient<ModelosDbService>();

            //container.Register<ModelosHttpService>();
        }
    }
}
