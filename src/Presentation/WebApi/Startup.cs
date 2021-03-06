namespace CleanArchitecture.WebApi
{
    using CleanArchitecture.Application;
    using CleanArchitecture.Application.Common.Interfaces.Services;
    using CleanArchitecture.Infrastructure.Persistence;
    using CleanArchitecture.Infrastructure.Shared;
    using CleanArchitecture.WebApi.Extensions.StartupExtensions;
    using CleanArchitecture.WebApi.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication()
                    .AddInfrastructurePersistence(Configuration)
                    .AddInfrastructureShared(Configuration);

            services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddMetricsExtension()
                    .AddOpenTelemetryExtension(Configuration, WebHostEnvironment)
                    .AddAuthenticationExtension(Configuration)
                    .AddDataProtectionKeysExtension(Configuration)
                    .AddForwardHeadersExtension(Configuration)
                    .AddHealthChecksExtension(Configuration)
                    .AddSwaggerExtension(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardHeadersExtension(Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseExceptionMiddlewareExtension();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMetricsExtension();

            app.UseSwaggerExtension(Configuration);

            app.UseHealthChecksExtension();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                         .RequireAuthorization();
            });
        }
    }
}
