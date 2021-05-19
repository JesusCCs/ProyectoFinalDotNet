using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ProyectoFinal.API.Extensions;

namespace ProyectoFinal.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}