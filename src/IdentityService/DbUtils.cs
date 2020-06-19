using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Storage;
using Serilog;
using Npgsql;
using Polly;

namespace Reshape.IdentityService
{
    public class DbUtils
    {
        public static void Migrate(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(typeof(DbUtils).Assembly.FullName));
            });

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<PersistedGrantDbContext>(); // Returns null if service is not registered
                var contextName = context.GetType().Name;
                try
                {
                    // If for whatever reason the Db is not available, catch exception and retry.
                    // Specifically, when running on Docker the app container is sometimes ready
                    // before the Db container, this makes the migration fail taking the whole app down with it :(
                    Policy.Handle<NpgsqlException>()
                            .WaitAndRetry(
                                retryCount: 10, // Raise this if db is exceptionally slow at starting up.
                                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Exponential backoff
                                onRetry: (ex, timeSpan, retry, ctx) =>
                                {
                                    Log.Warning(ex, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry}", contextName, ex.GetType().Name, ex.Message, retry);
                                })
                            .Execute(() =>
                            {
                                Log.Information("Attempting to apply migrations to database ({DbContext})", contextName);
                                context.Database.Migrate();
                            });

                    Log.Information("Successfully migrated database ({DbContext})", contextName);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while migrating the database ({DbContext})", contextName);
                }
            }
        }
    }
}
