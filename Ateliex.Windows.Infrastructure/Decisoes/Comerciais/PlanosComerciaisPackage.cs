using Microsoft.Extensions.DependencyInjection;

namespace Ateliex.Decisoes.Comerciais
{
    public class PlanosComerciaisPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<PlanosComerciaisInfraService>();

            services.AddTransient<PlanosComerciaisDbService>();

            //container.Register<PlanosComerciaisHttpService>();
        }
    }
}
