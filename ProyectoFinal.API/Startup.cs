using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoFinal.DAL;

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
            services.SetConfigurationAndEnvironment(_configuration, _env);

            services.AddControllers();
            
            services.AddIdentity();
            
            services.AddCors();
            
            services.AddSwagger();

            services.AddDependencies();
        }
        
        
        public void Configure(IApplicationBuilder app)
        {
            var cors = ServiceExtension.ProdCors;
            
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProyectoFinal.API v1"));
                cors = ServiceExtension.DevCors;
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