using Ateliex.Cadastro.Modelos;
using Ateliex.Decisoes.Comerciais;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ateliex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        readonly InfrastructurePackage infrastructurePackage;

        readonly Container container;

        public IServiceProvider ServiceProvider { get; private set; }

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            ServiceProvider = serviceProvider;

            infrastructurePackage = new InfrastructurePackage();

            container = new Container();

            //container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            container.RegisterPackages(assemblies);

            //container.Verify();

            //// Build an IServiceProvider with DbContext pooling and resolve a scope factory.
            //var scopeFactory = new ServiceCollection()
            //    .AddDbContextPool<BloggingContext>(o => o.UseSqlServer(connectionString))
            //    .BuildServiceProvider(validateScopes: true)
            //    .GetRequiredService<IServiceScopeFactory>();

            //// Use that scope factory to register an IServiceScope into Simple Injector
            //container.Register<IServiceScope>(scopeFactory.CreateScope, Lifestyle.Scoped);

            //// Cross wire the DbContext by resolving the IServiceScope and requesting the
            //// DbContext from that scope.
            //container.Register(() => container.GetInstance<IServiceScope>().ServiceProvider
            //    .GetService<BloggingContext>(),
            //    Lifestyle.Scoped);

            //// Start using Simple Injector as usual:
            //using (AsyncScopedLifestyle.BeginScope(container))
            //{
            //    var c = container.GetInstance<SomeComponentDependingOnBloggingContext>();
            //}
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //await infrastructurePackage.EnsureDatabaseCreatedAsync(container);
        }

        private void CadastroDeModelosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //var modelosLocalService = container.GetInstance<ModelosLocalService>();

            //var modelosWindow = new ModelosWindow(
            //    modelosLocalService
            //);

            var modelosWindow = ServiceProvider.GetRequiredService<ModelosWindow>();

            modelosWindow.Show();
        }

        private void PlanejamentoComercialMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var planosComerciaisLocalService = container.GetInstance<PlanosComerciaisLocalService>();

            var modelosLocalService = container.GetInstance<ModelosLocalService>();

            //var consultaDePlanosComerciais = container.GetInstance<IConsultaDePlanosComerciais>();

            //var planejamentoComercial = container.GetInstance<IPlanejamentoComercial>();

            var planosComerciaisForm = new PlanosComerciaisWindow(
                planosComerciaisLocalService,
                modelosLocalService
            //consultaDePlanosComerciais,
            //planejamentoComercial
            );

            planosComerciaisForm.Show();
        }
    }
}
