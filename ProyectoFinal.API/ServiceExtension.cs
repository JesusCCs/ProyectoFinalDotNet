using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.BL.Implementations;
using ProyectoFinal.DAL.Repositories.Contracts;
using ProyectoFinal.DAL.Repositories.Implementations;

namespace ProyectoFinal.API
{
    public class ServiceExtension
    {
        /// <summary>
        /// Constante para la configuración de CORS en local
        /// </summary>
        public const string DevCors = "DevCors";
        
        /// <summary>
        /// Constante para la configuración de CORS en producción
        /// </summary>
        public const string ProdCors = "ProdCors";
        
        public static void AddDependecies(IServiceCollection services)
        {
            // Automapper
            services.AddAutoMapper(typeof(MappingProfile));
            
            // Se añade inyección de dependencias de bl
            services.AddScoped<IGinmasioBl, GinmasioBl>();
            services.AddScoped<IAuthBl, AuthBl>();
            
            // Se añade inyección de dependencias de respositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepositoryAuth<>), typeof(RepositoryAuth<>));
        }

        public static void AddSwagger(IServiceCollection services)
        {
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

        public static void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DevCors,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                
                // TODO: cors para producción
            });
        }

        public static void AddIdentity(IServiceCollection services)
        {
            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    
                });
        }
    }
}