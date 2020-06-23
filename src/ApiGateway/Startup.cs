using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using IdentityServer4.AccessTokenValidation;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

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
            services.AddCors();

            IdentityModelEventSource.ShowPII = true; // DEV: For development only! Enables showing Personally Identifiable Information in logging.

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(opt =>
                {
                    opt.Authority = "http://identity.svc";
                    opt.ApiName = "gateway";
                    opt.ApiSecret = "secret";
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                });

            services.AddSwaggerForOcelot(Configuration);
            services.AddOcelot(Configuration)
                .AddDelegatingHandler<AddJWTHandler>(global: true); // Change reference token to jwt
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            logger.LogInformation("Startup Configuring... woah!");

            // TODO: properly configure CORS at some point when it starts being relevant.
            app.UseCors(opt =>
            {
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
                opt.AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseSwaggerForOcelotUI();
            app.UseOcelot().Wait();
        }
    }
}
