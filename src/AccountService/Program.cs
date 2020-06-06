using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Serilog;

using Reshape.AccountService.Infrastructure;
using Reshape.Common.Extensions;
using Reshape.Common.DevelopmentTools;

namespace Reshape.AccountService
{
    public static class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.') - 1);

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();
            var isDevelopment = configuration["ASPNETCORE_ENVIRONMENT"].ToLower() == "development";
            Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                var host = CreateHostBuilder(configuration, args).Build();

                // Run the migration between the build and run steps to ensure there are no attempts at using the db until after migration has finished.
                Log.Information("Applying migrations ({ApplicationContext})...", AppName);
                host.MigrateDatabase<AccountContext, NpgsqlException>();

                // Seed db if developing
                if (isDevelopment)
                {
                    Log.Information("Development mode detected! - Seeding database ({ApplicationContext})...", AppName);
                    host.SeedDatabase<AccountContext>();
                }

                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    })
                // Check link below when configuring this service for production
                // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1
                // .ConfigureWebHost(x =>
                //     x.UseStartup<Startup>()
                //      .UseContentRoot(Directory.GetCurrentDirectory())
                // )
                .UseSerilog();



        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
