using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Transactions;

namespace Ateliex
{
    public class InfrastructurePackage
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AteliexDbContext>(options =>
                options.UseSqlite(@"Data Source=Ateliex.db"));

            HttpClient client = new HttpClient();

            //var baseAdresse = ConfigurationManager.AppSettings["AtelieBaseAddress"].ToString();

            //client.BaseAddress = new Uri(baseAdresse);

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            services.AddSingleton(client);

            ServiceProvider = services.BuildServiceProvider();            

            //

            //var connectionString = ConfigurationManager.ConnectionStrings["Atelie"].ToString();

            //var builder = new DbContextOptionsBuilder<AtelieDbContext>();

            //container.Register(() => new AtelieDbContext(builder.UseSqlite(connectionString).Options), Lifestyle.Singleton);

            //container.Register<AteliexDbContext>(Lifestyle.Singleton);
        }

        public async Task EnsureDatabaseCreatedAsync(IServiceCollection services)
        {
            var serviceScopeFactory = ServiceProvider.GetRequiredService<IServiceScopeFactory>();

            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<AteliexDbContext>();

                await dbContext.Database.EnsureCreatedAsync();

                await dbContext.Database.MigrateAsync();
            }
        }
    }
}
