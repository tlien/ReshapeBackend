using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reshape.BusinessManagementService.Infrastructure;
using Reshape.Common.EventBus;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Reshape.Common.Extensions;

namespace Reshape.BusinessManagementService
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            var host = CreateHostBuilder(args).Build();
            host.MigrateDatabase<IntegrationEventLogContext, NpgsqlException>();
            host.MigrateDatabase<BusinessManagementContext, NpgsqlException>();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
              });
            
    }
}
