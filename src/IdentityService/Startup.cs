using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

using Reshape.IdentityService.Infrastructure;

namespace Reshape.IdentityService
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // Add MVC
            services.AddCors();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var authConfiguration = Configuration.GetSection("AuthConfiguration");
            var authSecretsConfiguration = Configuration.GetSection("AuthSecretsConfiguration");



            IdentityModelEventSource.ShowPII = true; // DEV: For development only! Enables showing Personally Identifiable Information in logging.

            var builder = services.AddIdentityServer(opt =>
                {
                    // Enable all events while in development. Might even be useful once structured logging is properly enabled.
                    opt.Events.RaiseErrorEvents = true;
                    opt.Events.RaiseInformationEvents = true;
                    opt.Events.RaiseFailureEvents = true;
                    opt.Events.RaiseSuccessEvents = true;
                    // Set IssuerUri of token issuer. Since docker handles hostname resolution, we have to write
                    // the hostname EXACTLY as written in the client. Also 'localhost' can't be used as IssuerUri
                    // even though that's what docker hostnames ultimately gets resolved to internally.
                    opt.IssuerUri = "http://identity.svc";
                })
                // DEV: Adds static predefined test users. This is only for development.
                .AddTestUsers(TestUsers.Users)
                // Adds in-memory configuration data (vs. persisted to a database as with the operational data below).
                // During development where these resources and clients change a lot, it is easiest to have them not persisted.
                .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis(authSecretsConfiguration))
                .AddInMemoryClients(Config.Clients(authConfiguration))
                // Adds operational data (codes, tokens, consents) from a database.
                .AddOperationalStore(opt =>
                {
                    opt.ConfigureDbContext = builder => builder.UseNpgsql(connectionString);
                    opt.EnableTokenCleanup = true; // Enables automatic token cleanup.
                })
                .AddExtensionGrantValidator<ExchangeReferenceTokenGrantValidator>();

            if (Environment.IsDevelopment())
            {
                // DEV: This is for development only!
                builder.AddDeveloperSigningCredential();
            }

            services.AddAuthentication();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            // TODO: properly configure CORS at some point when it starts being relevant.
            app.UseCors(opt =>
            {
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
                opt.AllowAnyOrigin();
            });
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
