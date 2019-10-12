using SimpleInjector;
using SimpleInjector.Packaging;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ModelosLocalService>();

            //container.Register<ModelosHttpService>();
        }
    }
}
