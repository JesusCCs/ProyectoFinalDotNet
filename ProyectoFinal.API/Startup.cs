using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.BL.Implementations;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Repositories.Contracts;
using ProyectoFinal.DAL.Repositories.Implementations;

namespace ProyectoFinal.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private const string DevCors = "DevCors"; // Vamos a permitir cualquiera para desarrollo en local
        private const string ProdCors = "ProdCors"; // Permitiremos solo ciertas url en producción
        
        private IConfiguration Configuration { get; }
        
        /// <summary>
        /// Constructor of the startup. Dependency injection for the configuration file
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            // Base de datos
            var connectionString = Configuration.GetConnectionString("DbConnection");
            services.AddDbContext<DataBaseContext>(o => 
                o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Automapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Para habilitar CORS
            services.AddCors(options =>
            {
                // Cors en local
                options.AddPolicy(DevCors,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                
                // TODO: cors para producción
            });
            
            // Se añade Swagger
            services.AddSwaggerGen(c =>
            {
                const string groupName = "v1";

                c.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = "ProyectoFinal.API", 
                    Version = groupName,
                    Description = "BackEnd del Proyecto Final de DAM"
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
            // Se añade inyección de dependencias de bl
            services.AddScoped<IGinmasioBl, GinmasioBl>();
            
            // Se añade inyección de dependencias de respositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepositoryAuth<>), typeof(RepositoryAuth<>));
        }
        
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cors = ProdCors;
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProyectoFinal.API v1"));
                cors = DevCors;
            }

            app.UseHttpsRedirection();
            
            // Permite tener archivos estáticos, que se acceden directamente, sin pasar por los controladores
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseCors(cors);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}