using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BusinessManagementService.API.Application.Queries.AnalysisProfileQueries;
using BusinessManagementService.API.Configuration;
using BusinessManagementService.Domain.AggregatesModel.AnalysisProfileAggregate;
using BusinessManagementService.Domain.AggregatesModel.BusinessTierAggregate;
using BusinessManagementService.Domain.AggregatesModel.FeatureAggregate;
using BusinessManagementService.Infrastructure;
using BusinessManagementService.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusinessManagementService
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
            services.AddEntityFrameworkNpgsql().AddDbContext<BusinessManagementContext>(options =>
                {
                    options.UseNpgsql(Configuration.GetConnectionString("DbContext"));
                }
            );

            services.AddScoped<IAnalysisProfileRepository, AnalysisProfileRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IBusinessTierRepository, BusinessTierRepository>();
            // services.AddScoped<IAnalysisProfileQueries>(_ => new AnalysisProfileQueries(Configuration.GetConnectionString("DbContext")));
            services.AddScoped<IAnalysisProfileQueries,  AnalysisProfileQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapGet("/", async context =>
            //     {
            //         await context.Response.WriteAsync("Hello from Business Management Service!");
            //     });
            // });

            app.UseMvc();
        }
    }
}
