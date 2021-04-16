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
            // Base de datos
            var connectionString = _configuration.GetConnectionString("DbConnection");
            services.AddDbContext<DataBaseContext>(o => 
                o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            
            services.AddControllers();
            
            ServiceExtension.AddIdentity(services);
            
            ServiceExtension.AddCors(services);
            
            ServiceExtension.AddSwagger(services);

            ServiceExtension.AddDependecies(services);
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