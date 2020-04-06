
using Microsoft.Extensions.DependencyInjection;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ModelosInfraService>();

            services.AddTransient<ModelosDbService>();

            //container.Register<ModelosHttpService>();
        }
    }
}
