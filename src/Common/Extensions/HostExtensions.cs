using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Reshape.Common.Extensions
{
    public static class HostExtensions
    {
        // This should not be used for production environments because of the reasons outlined here: https://github.com/dotnet/efcore/issues/19587
        // TODO: Add some retry logic in case database is not ready to do work.
        public static IHost MigrateDatabase<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var Services = scope.ServiceProvider;
                try
                {
                    var db = Services.GetRequiredService<TContext>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Do logging here once it's in place
                    throw;
                }
            }
            return host;
        }
    }
}