using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Reshape.BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;
using Reshape.BusinessManagementService.API.Application.Queries.BusinessTierQueries;
using Reshape.BusinessManagementService.API.Application.Queries.FeatureQueries;
using Reshape.BusinessManagementService.API.Configuration;
using Reshape.BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using Reshape.BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using Reshape.BusinessManagementService.Infrastructure;
using Reshape.BusinessManagementService.Infrastructure.Repositories;
using Reshape.Common.EventBus;
using Reshape.Common.EventBus.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MassTransit;
using Reshape.BusinessManagementService.API.Application.Behaviors;
using System.Data.Common;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Events;
using Reshape.BusinessManagementService.API.Application.IntegrationEvents.Consumers;
using GreenPipes;
using Microsoft.OpenApi.Models;

namespace Reshape.BusinessManagementService
{
    public class Startup
    {
        private IHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options =>
                    {
                        options.EnableEndpointRouting = false;
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson();

            services.AddSingleton(AutoMapperConfig.CreateMapper());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

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
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host("rabbitmq");

                    cfg.ReceiveEndpoint("newanalysisprofile_queue", e =>
                    {
                        e.UseMessageRetry(r => r.Interval(2, 100));
                        e.ConfigureConsumer<NewAnalysisProfileConsumer>(provider);
                        e.Consumer<NewAnalysisProfileConsumer>();
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMvc();
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
        }
    }
}
