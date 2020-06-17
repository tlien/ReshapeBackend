using System;
using System.Data.Common;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using GreenPipes;
using MassTransit;
using MediatR;

using Reshape.Common.EventBus;
using Reshape.Common.EventBus.Services;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;
using Reshape.AccountService.Infrastructure;
using Reshape.AccountService.Infrastructure.Repositories;
using Reshape.AccountService.API.Application.Behaviors;
using Reshape.AccountService.API.Application.IntegrationEvents.Consumers;
using Reshape.AccountService.API.Application.Queries.AccountQueries;
using Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries;
using IdentityServer4.AccessTokenValidation;

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
            services.AddCors();
            services.AddHealthChecks();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContexts(Configuration);
            services.AddCustomMediatR();
            services.AddEventBus();
            services.AddRepositories();
            services.AddCQRS();
            services.AddCustomControllers();
            services.AddSwagger();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
               .AddIdentityServerAuthentication(opt =>
               {
                   opt.Authority = "http://identity.svc";
                   opt.ApiName = "acc";
                   opt.ApiSecret = "!s3cr3t";
                   opt.RequireHttpsMetadata = false;
               });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Startup Configuring... woah!");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt =>
            {
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
                opt.AllowAnyOrigin();
            });

            app.UseRouting();

            // Auth goes between UseRouting and UseEndpoints!
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(ep =>
            {
                ep.MapControllers();

                ep.MapHealthChecks("/health/live", new HealthCheckOptions()
                {
                    // Exclude all checks and return a 200-Ok. Basically just a check to see if we can get requests through
                    Predicate = (_) => false
                });

                ep.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    // The readiness check uses all registered checks with the 'ready' tag.
                    Predicate = (check) => check.Tags.Contains("ready")
                });

            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reshape.AccountService API");
            });

            // ConfigureEvents(app);
        }

        public void ConfigureEvents(IApplicationBuilder app)
        {
            var eventTracker = app.ApplicationServices.GetRequiredService<IEventTracker>();
        }
    }

    static class CustomExtensionMethods
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AccountDbContext");

            services.AddDbContext<AccountContext>(opt =>
            {
                opt.UseNpgsql(connectionString, npgsqlOpt =>
                {
                    npgsqlOpt.EnableRetryOnFailure();
                })
                .UseSnakeCaseNamingConvention();
            });

            services.AddDbContext<IntegrationEventLogContext>(opt =>
            {
                opt.UseNpgsql(connectionString, npgsqlOpt =>
                {
                    npgsqlOpt.EnableRetryOnFailure();
                })
                .UseSnakeCaseNamingConvention();
            });

            return services;
        }

        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            return services;
        }

        public static IServiceCollection AddCQRS(this IServiceCollection services)
        {
            services.AddScoped<IAccountQueries, AccountQueries>();
            services.AddScoped<IAccountAdditionsQueries, AccountAdditionsQueries>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }

        public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<AnalysisProfileCreatedConsumer>();
                x.AddConsumer<BusinessTierCreatedConsumer>();
                x.AddConsumer<FeatureCreatedConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);

                    // Hand this whatever name the rabbitmq service has in the docker-compose file
                    cfg.Host("rabbitmq");

                    cfg.ReceiveEndpoint("analysis_profile_created_queue", e =>
                    {
                        e.UseMessageRetry(r => r.Interval(2, 100));
                        e.ConfigureConsumer<AnalysisProfileCreatedConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("business_tier_created_queue", e =>
                    {
                        e.UseMessageRetry(r => r.Interval(2, 100));
                        e.ConfigureConsumer<BusinessTierCreatedConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("feature_created_queue", e =>
                    {
                        e.UseMessageRetry(r => r.Interval(2, 100));
                        e.ConfigureConsumer<FeatureCreatedConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IIntegrationEventService, IntegrationEventService<AccountContext>>();

            services.AddSingleton<IEventTracker, EventTracker>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Reshape.AccountService API",
                    Version = "v1"
                });
                c.EnableAnnotations();
            });

            return services;
        }
    }
}
