using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace CleanArchitecture.WebApi.Extensions.HostBuilderExtensions
{
    public static class AppMetricsExtension
    {
        public static IHostBuilder UseAppMetricsHostBuilderExtension(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureMetricsWithDefaults(builder =>
                {
                    builder.OutputMetrics.AsPrometheusPlainText();
                })
                .UseMetrics(options =>
                {
                    options.EndpointOptions = endpointsOptions =>
                    {
                        endpointsOptions.MetricsTextEndpointOutputFormatter =
                            Metrics.Instance
                                    .OutputMetricsFormatters
                                    .OfType<MetricsPrometheusTextOutputFormatter>()
                                    .First();
                    };
                });
        }
    }
}
