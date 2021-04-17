using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProyectoFinal.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Iniciamos base...
            services.SetConfigurationAndEnvironment(_configuration, _env);

            // ...Controladores
            services.AddControllers();

            // ...Sistema de seguridad
            services.AddIdentity().AddJwt().AddCors();

            // ...Swagger para pruebas y documentación visual
            services.AddSwagger();

            // ...Inyección de dependencias
            services.AddDependencies();
        }
        
        
        public void Configure(IApplicationBuilder app)
        {
            var cors = ServicesExtension.ProdCors;
            
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProyectoFinal.API v1"));
                cors = ServicesExtension.DevCors;
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();
            
            app.UseCors(cors);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}