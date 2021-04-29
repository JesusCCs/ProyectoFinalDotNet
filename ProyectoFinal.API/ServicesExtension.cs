using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProyectoFinal.API.Authorization.Handlers;
using ProyectoFinal.API.Authorization.Requirements;
using ProyectoFinal.BL.Contracts;
using ProyectoFinal.BL.Implementations;
using ProyectoFinal.BL.Providers;
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
            
            // Automapper
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
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
                
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Escribir en value: Bearer {TOKEN}. Donde {TOKEN} es el AccessToken devuelto en el login",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, Array.Empty<string>()}
                });
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
            services.AddIdentity<Auth, Rol>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                    if (_env.IsDevelopment())
                    {
                        options.SignIn.RequireConfirmedEmail = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequireUppercase = false;
                    }

                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<DataBaseContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<RefreshTokenProvider<Auth>>(App.RefreshTokenProvider);

            return services;
        }
        
        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
            
            // Se configura JWT
            services.AddAuthentication(options =>  
                {  
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ValidAudience = jwtSettings.Audience,

                        ClockSkew = TimeSpan.Zero
                    };
                });
            
            // Configuración del tiempo de caducidad del RefreshToken
            services.Configure<RefreshTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(jwtSettings.RefreshTokenExpirationInDays);
            });
            
            // Añadimos las inyecciones de dependencias relacionadas con los JWT tokens
            services.AddSingleton(jwtSettings);
            services.AddScoped<IJwtTokenBl,JwtTokenBl>();

            return services;
        }
        
        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler,GymIsOwnerHandler>();
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.GymIsOwner, policy =>
                {
                    policy.Requirements.Add(new GymIsOwnerRequirement());
                });
            });
            
            return services;
        }

        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            var mailtrap = _configuration.GetSection("MailtrapSettings");
                
            services
                .AddFluentEmail(mailtrap["From"])
                .AddRazorRenderer(Directory.GetCurrentDirectory())
                .AddSmtpSender(new SmtpClient("smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential(mailtrap["User"], mailtrap["Key"]),
                    EnableSsl = true
                });

            return services;
        }
    }
}