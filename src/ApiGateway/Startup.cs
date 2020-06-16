using System;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

namespace Reshape.ApiGateway
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddCors();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(opt =>
                {
                    opt.Authority = "http://identity.svc";
                    opt.ApiName = "gateway";
                    opt.ApiSecret = "secret";
                    opt.RequireHttpsMetadata = false;
                    // opt.EnableCaching = true;
                    // opt.CacheDuration = TimeSpan.FromMinutes(10);
                });
            services.AddSwaggerForOcelot(Configuration);
            services.AddOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            logger.LogInformation("Startup Configuring... woah!");

            // app.UseSerilogRequestLogging();

            app.UseCors(opt =>
            {
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
                opt.AllowAnyOrigin();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerForOcelotUI();
            app.UseOcelot().Wait();
        }
    }
}
