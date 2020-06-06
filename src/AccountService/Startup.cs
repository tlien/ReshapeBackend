using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;

using Reshape.AccountService.Domain.AggregatesModel.AccountAggregate;
using Reshape.AccountService.Infrastructure;
using Reshape.AccountService.Infrastructure.Repositories;
using Reshape.AccountService.API.Infrastructure.AutoMapper;
using Reshape.AccountService.API.Application.Queries.AccountQueries;
using Reshape.AccountService.API.Application.Queries.AccountAdditionsQueries;

namespace Reshape.AccountService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomDbContext(Configuration);
            services.AddMvc(options => options.EnableEndpointRouting = false).AddNewtonsoftJson();
            services.AddSingleton(AutoMapperConfig.CreateMapper());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountQueries, AccountQueries>();
            services.AddScoped<IAccountAdditionsQueries, AccountAdditionsQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }

    static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccountContext>(opt =>
            {
                opt.UseNpgsql(configuration["ConnectionString"]);
            },
                ServiceLifetime.Scoped
            );

            return services;
        }
    }
}
