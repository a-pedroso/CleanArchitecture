using CleanArchitecture.Application;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Shared;
using CleanArchitecture.WebApi.Helpers;
using CleanArchitecture.WebApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CleanArchitecture.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();

            services.AddInfrastructurePersistence(Configuration);
            
            services.AddInfrastructureShared(Configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddControllers()
                    .AddMetrics()
                    .AddFluentValidation();

            // CPU, memory & GC
            services.AddAppMetricsCollectors();

            // For pushing metrics to reporters like console, file, influxDb, etc.
            //services.AddMetricsReportingHostedService();

            services.AddSwaggerExtension();

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerExtension();
        }
    }
}
