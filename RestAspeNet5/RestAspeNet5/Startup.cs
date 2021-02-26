using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestAspeNet5.Business;
using RestAspeNet5.Business.Implementacao;
using RestAspeNet5.Hypermedia.Enricher;
using RestAspeNet5.Hypermedia.Filters;
using RestAspeNet5.Modals.Context;
using RestAspeNet5.Repository;
using RestAspeNet5.Repository.Generic;
using RestAspeNet5.Service;
using RestAspeNet5.Configuration;
using RestAspeNet5.Service.Implementations;
using Serilog;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            var TokenConfiguration = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations")
                ).Configure(TokenConfiguration);

            services.AddSingleton(TokenConfiguration);
            _ = services.AddAuthentication(opt =>
              {
                  opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              }).AddJwtBearer(Options =>
              {
                  Options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = TokenConfiguration.Issuer,
                      ValidAudience = TokenConfiguration.Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfiguration.Secret))
                  };
              });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build()
                    );
            });
            //Cors
            services.AddCors(opt=> opt.AddDefaultPolicy(builder=> { 
                builder.AllowAnyOrigin()
                .AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddControllers();
            //Injetando Mysql Conexão
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));
            //Configurando Migração
            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            //Formatando para formato Xml
            services.AddMvc(opt =>
            {
                opt.RespectBrowserAcceptHeader = true;

                opt.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                opt.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            //Adicionando Hateos
            var filteroptions = new HyperMidiaFilterOptions();
            filteroptions.ContentResponsiveEnricherList.Add(new PersonEnricher());
            filteroptions.ContentResponsiveEnricherList.Add(new BookEnricher());

            services.AddSingleton(filteroptions);
            //Swagger
            services.AddSwaggerGen(Sw=> {
                Sw.SwaggerDoc("v1", new OpenApiInfo {
                    Title="Rest Api from 0 To Azure",
                    Version="v1",
                    Description="Learning .NET in developer course",
                    Contact = new OpenApiContact
                    {
                        Name="Josia Almeida",
                        Url= new Uri("http://github.com/JosiaAlmeida")
                    }
                });
            });
            //Injeção de dependencias
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddApiVersioning();
            //Injetando Services
            //Injetando nossa classe de negocio
            services.AddScoped<IPersonBusiness, PersonImplementationBusiness>();
            services.AddScoped<IBooksBusiness, BookImplementationBusiness>();
            services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
            services.AddScoped<IFileBusiness, FileBusinessImplementation>();

            services.AddTransient<ITokenService, TokenServices>();

            services.AddScoped<IUsersRepository,UsersRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
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
            //Habilitando cors
            //Depois de Https e routing, e antes de endpoints
            app.UseCors();

            app.UseSwagger();
            //Gera uma pagina Html
            app.UseSwaggerUI(Sw=> {
                Sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest Api from 0 To Azure - v1");
                }
            );

            var opt = new RewriteOptions();
            opt.AddRedirect("^$", "swagger");
            app.UseRewriter(opt);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //Hateos
                endpoints.MapControllerRoute("Defaultapi", ("{controller=value}/{id?}"));
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
