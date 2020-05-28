using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;

namespace Reshape.Reshape.Common.Extensions
{
    public static class HostExtensions
    {
        /// <summary>
        ///     Attempts to apply migrations to the specified database at runtime, before the
        ///     the application attempts to use the datebase. This is intended to be used for
        ///     applying migrations in an environment where the database resides in a separate
        ///     container from the application. If the database container is not ready when the
        ///     migration is attempted, it will be retried 3 times at 3,5 and 8 second intervals.
        ///     Triggers retrying on SqlException specifically (PostgreSQL uses a different exception).
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
                var Services = scope.ServiceProvider;
                try
                {
                    var db = Services.GetRequiredService<TDbContext>();

                    // If for whatever reason the Db is not available, catch exception and retry.
                    // Specifically, the app container is sometimes ready before the Db container, this makes the migrate fail, taking the whole app down with it.
                    Policy.Handle<TException>()
                        .WaitAndRetry(new TimeSpan[] {
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(8),
                        })
                        .Execute(() => db.Database.Migrate());
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