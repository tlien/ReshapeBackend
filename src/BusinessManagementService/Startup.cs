using System;
using System.Data.Common;
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
using MassTransit;
using MediatR;

using Reshape.Common.EventBus;
using Reshape.Common.EventBus.Services;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Reshape.BusinessManagementService.Infrastructure;
using Reshape.BusinessManagementService.Infrastructure.Repositories;
using Reshape.BusinessManagementService.API.Application.Behaviors;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;
using Reshape.BusinessManagementService.API.Application.Queries.BusinessTierQueries;
using Reshape.BusinessManagementService.API.Application.Queries.FeatureQueries;
using IdentityServer4.AccessTokenValidation;

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

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
               .AddIdentityServerAuthentication(opt =>
               {
                   opt.Authority = "http://identity.svc";
                   opt.ApiName = "bm";
                   opt.ApiSecret = "s3cr3t";
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
            app.UseAuthentication();
            app.UseAuthorization();

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reshape.BusinessManagementService API");
            });

            ConfigureEvents(app);
        }

        public void ConfigureEvents(IApplicationBuilder app)
        {
            var eventTracker = app.ApplicationServices.GetRequiredService<IEventTracker>();

            eventTracker.AddEventType<AnalysisProfileCreatedEvent>();
            eventTracker.AddEventType<BusinessTierCreatedEvent>();
            eventTracker.AddEventType<FeatureCreatedEvent>();
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

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
               sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IIntegrationEventService, IntegrationEventService<BusinessManagementContext>>();

            services.AddSingleton<IEventTracker, EventTracker>();

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
            });

            return services;
        }
    }
}
