using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using MassTransit;
using MediatR;

using Reshape.Common.DevelopmentTools;
using Reshape.Common.EventBus;
using Reshape.Common.EventBus.Services;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Reshape.BusinessManagementService.Infrastructure;
using Reshape.BusinessManagementService.Infrastructure.Repositories;
using Reshape.BusinessManagementService.API.Application.Behaviors;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileExtrasQueries;
using Reshape.BusinessManagementService.API.Application.Queries.BusinessTierQueries;
using Reshape.BusinessManagementService.API.Application.Queries.FeatureQueries;

namespace Reshape.BusinessManagementService
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

            IdentityModelEventSource.ShowPII = true; // DEV: For development only! Enables showing Personally Identifiable Information in logging.

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(opt =>
                {
                    opt.Authority = "http://identity.svc";
                    opt.ApiName = "bm";
                    opt.ApiSecret = "!s3cr3t";
                    opt.RequireHttpsMetadata = false;
                    opt.SupportedTokens = SupportedTokens.Both;
                    opt.SaveToken = true;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            logger.LogInformation("Startup Configuring... woah!");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // TODO: properly configure CORS at some point when it starts being relevant.
            app.UseCors(opt =>
            {
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
                opt.AllowAnyOrigin();
            });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reshape.BusinessManagementService API");
                c.OAuthClientId("rshp.bm.swagger");
                c.OAuthClientSecret("!s3cr3t");
                c.OAuthAppName("Business Management Swagger");
                c.OAuthUsePkce();
            });
        }
    }

    static class CustomExtensionMethods
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbContext");

            services.AddDbContext<BusinessManagementContext>(opt =>
            {
                opt.UseNpgsql(connectionString, npgsqlOpt =>
                {
                    npgsqlOpt.EnableRetryOnFailure();
                })
                .UseSnakeCaseNamingConvention(); // sets up tables and columns with snake_case automagically
            });

            // This is used to migrate the database while building the host.
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
            services.AddScoped<IAnalysisProfileQueries, AnalysisProfileQueries>();
            services.AddScoped<IAnalysisProfileExtrasQueries, AnalysisProfileExtrasQueries>();
            services.AddScoped<IBusinessTierQueries, BusinessTierQueries>();
            services.AddScoped<IFeatureQueries, FeatureQueries>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAnalysisProfileRepository, AnalysisProfileRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IBusinessTierRepository, BusinessTierRepository>();

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
            // ### MassTransit setup ###
            // Make sure that whatever your consumers are consuming is an IntegrationEvent type, as all integration events extend that class.
            // Published message and consumed message must be the same, otherwise the message will be skipped by RabbitMQ.
            // ReceiveEndpoint must be the name of the integration event (without the IntegrationEvent suffix) followed by "_queue",
            // e.g. "newanalysisprofile_queue".
            // You may configure as many endpoints as needed for all incoming integration event type messages.
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host("rabbitmq");
                }));
            });

            services.AddMassTransitHostedService();

            // Register integration event log as an ImplementationFactory.
            // DbConnection is provided by the service depending on this service (see IntegrationEventService).
            // The DbContext is managed internally by the IntegrationLogService independent of the IOC provider.
            // See IIntegrationEventLogService summary for more info.
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IIntegrationEventService, IntegrationEventService<BusinessManagementContext>>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Reshape.BusinessManagementService API",
                    Version = "v1"
                });
                c.EnableAnnotations();
                c.OperationFilter<AuthorizeOperationFilter>();
                c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("http://localhost:5200/connect/authorize"),
                            TokenUrl = new Uri("http://localhost:5200/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "openid scope" },
                                { "profile", "profile scope"},
                                { "role", "role scope"},
                                // { "acc", "acc scope"},
                                { "bm", "bm scope"},
                            },
                        }
                    },
                    Description = "Business Management Service OpenId Scheme"
                });

                // Add XML comments to Swagger api documentation
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
