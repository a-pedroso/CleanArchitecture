namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Prometheus;

    public static class HealthChecksExtension
    {
        public static IServiceCollection AddHealthChecksExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    //TODO: add health check dependencies
                    .ForwardToPrometheus();

            return services;
        }

        public static IApplicationBuilder UseHealthChecksExtension(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                //endpoints.MapHealthChecks("/health");
            });

            return app;
        }
    }
}