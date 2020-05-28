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

namespace Reshape.BusinessManagementService
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            var host = CreateHostBuilder(args);
            host.MigrateDbContext<IntegrationEventLogContext>();
            host.MigrateDbContext<BusinessManagementContext>();
            host.Run();
        }

        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
            
    }
}
