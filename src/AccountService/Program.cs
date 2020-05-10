using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Reshape.AccountService.Infrastructure;
using Reshape.Common.Extensions;

namespace Reshape.AccountService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Run the migration between the build and run steps to ensure there are no attempts at using the db until after migration has finished
            CreateHostBuilder(args).Build().MigrateDatabase<AccountContext, NpgsqlException>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
