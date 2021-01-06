using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    public static class AppMetricsExtension
    {
        public static IServiceCollection AddAppMetricsExtension(this IServiceCollection services, IConfiguration configuration)
        {
            // CPU, memory & GC
            services.AddAppMetricsCollectors();

            // For pushing metrics to reporters like console, file, influxDb, etc.
            //services.AddMetricsReportingHostedService();

            return services;
        }
    }
}