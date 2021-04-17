﻿using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.BL.Implementations;
using ProyectoFinal.DAL;
using ProyectoFinal.DAL.Models.Auth;
using ProyectoFinal.DAL.Repositories.Contracts;
using ProyectoFinal.DAL.Repositories.Implementations;

namespace ProyectoFinal.API
{
    public static class ServicesExtension
    {
        /// <summary>
        /// Constante para la configuración de CORS en local
        /// </summary>
        public const string DevCors = "DevCors";

        /// <summary>
        /// Constante para la configuración de CORS en producción
        /// </summary>
        public const string ProdCors = "ProdCors";
        
        private static IConfiguration _configuration;
        private static IWebHostEnvironment _env;

        public static IServiceCollection SetConfigurationAndEnvironment(
            this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            // Aplicamos variables
            _configuration = configuration;
            _env = env;
            
            // Base de datos
            var connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<DataBaseContext>(o =>
                o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Automapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Se añade inyección de dependencias de bl
            services.AddScoped<IGinmasioBl, GinmasioBl>();
            services.AddScoped<IAuthBl, AuthBl>();

            // Se añade inyección de dependencias de repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepositoryAuth<>), typeof(RepositoryAuth<>));

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
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

            return services;
        }

        public static IServiceCollection AddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DevCors,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

                // TODO: cors para producción
            });

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Auth, Rol>()
                .AddEntityFrameworkStores<DataBaseContext>()
                .AddDefaultTokenProviders();
            
            return services;
        }
        
        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            var jwtOptions = _configuration.GetSection("Jwt");
            
            services.AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtOptions.GetValue<string>("Issuer"),
                        ValidAudience = jwtOptions.GetValue<string>("Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.GetValue<string>("Secret"))),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}