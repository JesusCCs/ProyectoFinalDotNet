using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoFinal.DAL;

namespace ProyectoFinal.API
{
    public static class MigrationExtension
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            appContext.Database.Migrate();
            return host;
        }
    }
}