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
using GreenPipes;

using Reshape.Common.EventBus;
using Reshape.Common.EventBus.Services;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Reshape.BusinessManagementService.Infrastructure;
using Reshape.BusinessManagementService.Infrastructure.Repositories;
using Reshape.BusinessManagementService.API.Application.Behaviors;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Consumers;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;
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
            services.AddHealthChecks();
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // ### MassTransit setup ###
            // Consumer configurations are only here for show. BusinessManagementContext will not be consuming any integration event messages.
            // Make sure that whatever your consumers are consuming is an IntegrationEvent type, as all integration events extend that class.
            // Published message and consumed message must be the same, otherwise the message will be skipped by RabbitMQ.
            // ReceivedEndpoint must be the name of the integration event (without the integrationevent affix) followed by "_queue",
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

            // UseSnakeCaseNamingConvention() sets up tables and columns with snake case without explicitly renaming everything in entitytypeconfigurations
            services.AddDbContext<BusinessManagementContext>(options =>
                {
                    options
                        .UseNpgsql(Configuration.GetConnectionString("DbContext"), npgsqlOptions =>
                            {
                                npgsqlOptions.EnableRetryOnFailure();
                            }
                        )
                        .UseSnakeCaseNamingConvention();
                }
            );

            services.AddDbContext<IntegrationEventLogContext>(options =>
                {
                    options
                        .UseNpgsql(Configuration.GetConnectionString("DbContext"), npgsqlOptions =>
                            {
                                npgsqlOptions.EnableRetryOnFailure();
                            }
                        )
                        .UseSnakeCaseNamingConvention();
                }
            );

            services.AddScoped<IAnalysisProfileRepository, AnalysisProfileRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IBusinessTierRepository, BusinessTierRepository>();
            services.AddScoped<IAnalysisProfileQueries, AnalysisProfileQueries>();
            services.AddScoped<IBusinessTierQueries, BusinessTierQueries>();
            services.AddScoped<IFeatureQueries, FeatureQueries>();

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
               sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IBusinessManagementIntegrationEventService, BusinessManagementIntegrationEventService>();

            services.AddSingleton<IEventTracker, EventTracker>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reshape.BusinessManagementService API", Version = "v1" });
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

            eventTracker.AddEventType<NewAnalysisProfileIntegrationEvent>();
            eventTracker.AddEventType<NewBusinessTierIntegrationEvent>();
        }
    }
}
