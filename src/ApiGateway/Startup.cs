using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            // Console.WriteLine("Running in environment: {0}", env.EnvironmentName);

            // var builder = new ConfigurationBuilder()
            //     .SetBasePath(env.ContentRootPath)
            //     .AddJsonFile("appsettings.json", true, true)
            //     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
            //     .AddJsonFile($"configuration.{env.EnvironmentName}.json")
            //     .AddEnvironmentVariables();

            // Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseOcelot().Wait();
        }
    }
}
