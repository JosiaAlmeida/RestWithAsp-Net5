using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RestAspeNet5.Business;
using RestAspeNet5.Business.Implementacao;
using RestAspeNet5.Modals.Context;
using RestAspeNet5.Service;
using RestAspeNet5.Service.Implementacao;
using Serilog;

namespace RestAspeNet5
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        //Configurando Environment
        public IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            //Configurando Login de serilog
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //Injetando Mysql Conexão
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            //Configurando Migração
            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            services.AddApiVersioning();
            //Injetando Services
            services.AddScoped<IPersonService, PersonImplementationService>();
            //Injetando nossa classe de negocio
            services.AddScoped<IPersonBusiness, PersonImplementationBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestAspeNet5 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void MigrateDatabase(string connection)
        {
            try
            {
                //Base de dados conexão
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                //Inicializando o Evolve
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    //Rota para as migrations
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Erro na migração da base de dados", ex);
                throw;
            }
        }
    }
}
