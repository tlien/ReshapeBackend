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
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Reshape.BusinessManagementService
{
    public class Startup
    {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            services.AddEntityFrameworkNpgsql();
            
            // UseSnakeCaseNamingConvention() sets up tables and columns with snake case without explicitly renaming everything in entitytypeconfigurations
            services.AddDbContext<BusinessManagementContext>(options =>
                {
                    options
                        .UseNpgsql(Configuration.GetConnectionString("DbContext"))
                        .UseSnakeCaseNamingConvention();
                }
            );

            services.AddDbContext<IntegrationEventLogContext>(options =>
                {
                    options
                        .UseNpgsql(Configuration.GetConnectionString("DbContext"))
                        .UseSnakeCaseNamingConvention();
                }
            );

            services.AddScoped<IAnalysisProfileRepository, AnalysisProfileRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IBusinessTierRepository, BusinessTierRepository>();
            services.AddScoped<IAnalysisProfileQueries, AnalysisProfileQueries>();
            services.AddScoped<IBusinessTierQueries, BusinessTierQueries>();
            services.AddScoped<IFeatureQueries, FeatureQueries>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            app.UseMvc();
        }
    }
}
