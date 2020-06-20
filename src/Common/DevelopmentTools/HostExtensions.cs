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
                var logger = services.GetRequiredService<ILogger<TDbContext>>();
                var context = services.GetService<TDbContext>();
                var contextName = context.GetType().Name;

                try
                {
                    logger.LogInformation("Attempting to seed database ({DbContext}) with development data", contextName);
                    context.AddSeedData();
                    logger.LogInformation("Successfully seeded database ({DbContext})", contextName);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database ({DbContext})", contextName);
                }
            }
            return host;
        }
    }
}