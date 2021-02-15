namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using OpenTelemetry.Exporter;
    using OpenTelemetry.Resources;
    using OpenTelemetry.Trace;
    using System;

    public static class OpenTelemetryExtension
    {
        public static IServiceCollection AddOpenTelemetryExtension(this IServiceCollection services, 
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            if (string.Equals(configuration["OpenTelemetryConfig:Enabled"], "true", StringComparison.OrdinalIgnoreCase))
            {
                var otlpReceiverUri = configuration["OpenTelemetryConfig:OtlpReceiverUri"];

                services.AddOpenTelemetryTracing(
                            builder =>
                            {
                                builder.SetResourceBuilder(ResourceBuilder
                                       .CreateDefault()
                                       .AddService(webHostEnvironment.ApplicationName))
                                       .AddAspNetCoreInstrumentation()
                                       .AddOtlpExporter(options => options.Endpoint = new Uri(otlpReceiverUri));

                                if (webHostEnvironment.IsDevelopment())
                                {
                                    builder.AddConsoleExporter(options => options.Targets = ConsoleExporterOutputTargets.Debug);
                                }
                            });
            }

            return services;
        }
    }
}
