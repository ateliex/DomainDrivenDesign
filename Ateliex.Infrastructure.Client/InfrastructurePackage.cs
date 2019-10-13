using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Packaging;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Transactions;

namespace Ateliex
{
    public class InfrastructurePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            HttpClient client = new HttpClient();

            //var baseAdresse = ConfigurationManager.AppSettings["AtelieBaseAddress"].ToString();

            //client.BaseAddress = new Uri(baseAdresse);

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            container.RegisterInstance(client);

            //

            //var connectionString = ConfigurationManager.ConnectionStrings["Atelie"].ToString();

            //var builder = new DbContextOptionsBuilder<AtelieDbContext>();

            //container.Register(() => new AtelieDbContext(builder.UseSqlite(connectionString).Options), Lifestyle.Singleton);

            //container.Register<AteliexDbContext>(Lifestyle.Singleton);
        }

        public async Task EnsureDatabaseCreatedAsync(Container container)
        {
            var context = container.GetInstance<AteliexDbContext>();

            await context.Database.MigrateAsync();

            //await context.Database.EnsureCreatedAsync();
        }
    }
}
