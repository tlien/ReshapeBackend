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
using GreenPipes;
using MassTransit;
using MediatR;

using Reshape.Common.EventBus;
using Reshape.Common.EventBus.Services;
using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;
using Reshape.AccountService.Infrastructure;
using Reshape.AccountService.Infrastructure.Repositories;
using Reshape.AccountService.API.Application.Behaviors;
using Reshape.AccountService.API.Application.IntegrationEvents.Events;
using Reshape.AccountService.API.Application.IntegrationEvents.Consumers;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContexts(Configuration);
            services.AddCustomMediatR();
            services.AddEventBus();
            services.AddRepositories();
            services.AddCQRS();
            services.AddCustomControllers();
            services.AddSwagger();
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

            ConfigureEvents(app);
        }

        public void ConfigureEvents(IApplicationBuilder app)
        {
            var eventTracker = app.ApplicationServices.GetRequiredService<IEventTracker>();

            eventTracker.AddEventType<NewAnalysisProfileIntegrationEvent>();
            eventTracker.AddEventType<NewBusinessTierIntegrationEvent>();
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
            // ### MassTransit setup ###
            // Make sure that whatever your consumers are consuming is an IntegrationEvent type, as all integration events extend that class.
            // Published message and consumed message must be the same, otherwise the message will be skipped by RabbitMQ.
            // ReceiveEndpoint must be the name of the integration event (without the IntegrationEvent suffix) followed by "_queue",
            // e.g. "newanalysisprofile_queue".
            // You may configure as many endpoints as needed for all incoming integration event type messages.
            services.AddMassTransit(x =>
            {
                x.AddConsumer<NewAnalysisProfileConsumer>();
                x.AddConsumer<NewBusinessTierConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host("rabbitmq");

                    cfg.ReceiveEndpoint("newanalysisprofile_queue", e =>
                    {
                        e.UseMessageRetry(r => r.Interval(2, 100));
                        e.ConfigureConsumer<NewAnalysisProfileConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("newbusinesstier_queue", e =>
                    {
                        e.UseMessageRetry(r => r.Interval(2, 100));
                        e.ConfigureConsumer<NewBusinessTierConsumer>(provider);
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
