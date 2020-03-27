using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Ateliex.Cadastro.Modelos;
using Ateliex.Cadastro.Modelos.CadastroDeModelos;
using Ateliex.Cadastro.Modelos.ConsultaDeModelos;
using Ateliex.Decisoes.Comerciais;
using Ateliex.Decisoes.Comerciais.ConsultaDePlanosComerciais;
using Ateliex.Decisoes.Comerciais.PlanejamentoComercial;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ateliex
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AteliexDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddTransient(typeof(IUnitOfWork), typeof(TransactionScopeManager));

            services.AddTransient<ICadastroDeModelos, CadastroDeModelos>();

            services.AddTransient<IConsultaDeModelos, ModelosDbService>();

            services.AddTransient<IRepositorioDeModelos, ModelosDbService>();

            services.AddTransient<IConsultaDePlanosComerciais, PlanosComerciaisDbService>();

            services.AddTransient<IRepositorioDePlanosComerciais, PlanosComerciaisDbService>();

            services.AddTransient<IPlanejamentoComercial, PlanejamentoComercial>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
