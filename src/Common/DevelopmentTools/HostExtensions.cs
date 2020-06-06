using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Reshape.Common.DevelopmentTools
{
    public static class HostExtensions
    {
        public static IHost SeedDatabase<TDbContext>(this IHost host) where TDbContext : DbContext, ISeeder<TDbContext>
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TDbContext>>(); // Throws exception if service is not registered
                var context = services.GetService<TDbContext>(); // Returns null if service is not registered
                var dbname = context.Database.GetDbConnection().Database;

                try
                {
                    logger.LogInformation("Seeding database ({DatabaseName}) with default data -- THIS SHOULD ONLY HAPPEN WHEN DEVELOPING! --", dbname);
                    context.AddSeedData();
                    logger.LogInformation("Successfully seeded database ({DatabaseName})", dbname);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database ({DatabaseName})", dbname);
                }
            }
            return host;
        }
    }
}