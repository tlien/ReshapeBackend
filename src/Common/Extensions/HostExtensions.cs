using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace Reshape.Common.Extensions
{
    public static class HostExtensions
    {
        /// <summary>
        ///     Attempts to apply migrations to the specified database at runtime, before the
        ///     the application attempts to use the datebase. This is intended to be used for
        ///     applying migrations in an environment where the database resides in a separate
        ///     container from the application. If the database container is not ready when the
        ///     migration is attempted, it will be retried 3 times at 3,5 and 8 second intervals.
        ///     Triggers retrying on <c>SqlException</c> specifically (PostgreSQL uses a different exception).
        ///     This should probably not be used for production environments because of reasons
        ///     outlined at: https://github.com/dotnet/efcore/issues/19587
        /// </summary>
        /// <typeparam name="TDbContext">The type containing the database context.</typeparam>
        /// <returns>The same instance of the Microsoft.Extensions.Hosting.IHostBuilder for chaining.</returns>
        public static IHost MigrateDatabase<TDbContext>(this IHost host) where TDbContext : DbContext
        {
            return MigrateDatabase<TDbContext, SqlException>(host);
        }

        /// <summary>
        ///     Attempts to apply migrations to the specified database at runtime, before the
        ///     the application attempts to use the datebase. This is intended to be used for
        ///     applying migrations in an environment where the database resides in a separate
        ///     container from the application. If the database container is not ready when the
        ///     migration is attempted, it will be retried 3 times at 3,5 and 8 second intervals.
        ///     This should probably not be used for production environments because of reasons
        ///     outlined at: https://github.com/dotnet/efcore/issues/19587
        /// </summary>
        /// <typeparam name="TDbContext">The type containing the database context.</typeparam>
        /// <typeparam name="TException">The type containing the exception thrown when migration fails due to the database container not being ready.</typeparam>
        /// <returns>The same instance of the Microsoft.Extensions.Hosting.IHostBuilder for chaining.</returns>
        public static IHost MigrateDatabase<TDbContext, TException>(this IHost host) where TDbContext : DbContext where TException : DbException
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TDbContext>>(); // Throws exception if service is not registered
                var context = services.GetService<TDbContext>(); // Returns null if service is not registered
                var dbname = context.Database.GetDbConnection().Database;

                try
                {
                    // If for whatever reason the Db is not available, catch exception and retry.
                    // Specifically, when running on Docker the app container is sometimes ready
                    // before the Db container, this makes the migration fail taking the whole app down with it :(
                    Policy.Handle<TException>()
                            .WaitAndRetry(
                                retryCount: 10, // Raise this if db is exceptionally slow at starting up.
                                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Exponential backoff
                                onRetry: (exception, timeSpan, retry, ctx) =>
                                {
                                    logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry}", nameof(TDbContext), exception.GetType().Name, exception.Message, retry);
                                })
                            .Execute(() =>
                                {
                                    logger.LogInformation("Attempting to apply migrations to database ({DatabaseName})", dbname);
                                    context.Database.Migrate();
                                });

                    logger.LogInformation("Successfully migrated database ({DatabaseName})", dbname);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database ({DatabaseName})", dbname);
                }
            }
            return host;
        }

        /// <summary>
        ///     Conditionally run method of T.
        /// </summary>
        /// <param name="cond">Condition to check.</param>
        /// <param name="method">Method to run if <paramref name="cond"/> is True.</param>
        /// <returns>The same instance of T for further chaining.</returns>
        public static T If<T>(this T t, bool cond, Func<T, T> method)
        {
            if (cond)
                return method(t);
            return t;
        }
    }
}