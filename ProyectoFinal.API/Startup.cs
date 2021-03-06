using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ProyectoFinal.API
{
    public class Startup
    {
        private const string DevCors = "DevCors"; // Vamos a permitir cualquiera para desarrollo en local
        private const string ProdCors = "ProdCors"; // Permitiremos solo ciertas url en producción
        
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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