using Ateliex.Cadastro.Modelos;
using Ateliex.Decisoes.Comerciais;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;

namespace Ateliex
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        public App()
        {
            var culture = CultureInfo.CreateSpecificCulture("pt-BR");

            Thread.CurrentThread.CurrentCulture = culture;

            Thread.CurrentThread.CurrentUICulture = culture;

            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();


            InitializeDatabase();


            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AteliexDbContext>(options =>
                options.UseSqlite(@"Data Source=Ateliex.db"));

            services.AddTransient(typeof(IUnitOfWork), typeof(TransactionScopeManager));

            services.AddTransient(typeof(MainWindow));

            services.AddTransient<ModelosService>();

            services.AddTransient<ModelosDbService>();

            services.AddTransient(typeof(ModelosWindow));
            
            services.AddTransient(typeof(ConsultaDeModelosWindow));
            
            services.AddTransient(typeof(PlanosComerciaisWindow));

            services.AddTransient<PlanosComerciaisService>();

            services.AddTransient<PlanosComerciaisDbService>();
        }

        private void InitializeContainer()
        {
            var package = new InfrastructurePackage();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //container.RegisterPackages(assemblies);

            //container.Verify();
        }

        private void InitializeDatabase()
        {
            var serviceScopeFactory = ServiceProvider.GetRequiredService<IServiceScopeFactory>();

            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<AteliexDbContext>();

                dbContext.Database.EnsureCreated();

                dbContext.Database.Migrate();
            }
        }
    }
}
