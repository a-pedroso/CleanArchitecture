namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Prometheus;
    using Prometheus.SystemMetrics;

    public static class MetricsExtension
    {
        public static IServiceCollection AddMetricsExtension(this IServiceCollection services)
        {
            // CPU, memory & GC
            services.AddSystemMetrics();

            return services;
        }


        public static IApplicationBuilder UseMetricsExtension(this IApplicationBuilder app)
        {
            // must be after Routing, Authentication and Authorization
            // https://github.com/prometheus-net/prometheus-net#aspnet-core-http-request-metrics
            app.UseHttpMetrics();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
            });

            return app;
        }
    }
}