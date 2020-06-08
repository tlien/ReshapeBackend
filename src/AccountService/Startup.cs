using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;
using Reshape.AccountService.Infrastructure;
using Reshape.AccountService.Infrastructure.Repositories;
using Reshape.AccountService.API.Application.Queries.AccountQueries;
using Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries;

namespace Reshape.AccountService
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
            services.AddHealthChecks();
            services.AddCustomDbContext(Configuration);
            services.AddCustomControllers();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountQueries, AccountQueries>();
            services.AddScoped<IAccountAdditionsQueries, AccountAdditionsQueries>();

            services.AddCustomSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Startup Configuring... woah!");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Auth goes between UseRouting and UseEndpoints!
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reshape.AccountService API");
            });
        }
    }

    static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccountContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("AccountDbContext"));
            },
                ServiceLifetime.Scoped
            );

            return services;
        }

        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reshape.AccountService API", Version = "v1" });
                c.EnableAnnotations();
            });

            return services;
        }
    }
}
