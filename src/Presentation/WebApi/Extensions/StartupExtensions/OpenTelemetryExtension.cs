namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using OpenTelemetry.Exporter;
    using OpenTelemetry.Resources;
    using OpenTelemetry.Trace;
    using StackExchange.Redis;
    using System;

    public static class OpenTelemetryExtension
    {
        // https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/examples/Console/TestJaegerExporter.cs
        public static IServiceCollection AddOpenTelemetryExtension(this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            if (string.Equals(configuration["OpenTelemetryConfig:Enabled"], "true", StringComparison.OrdinalIgnoreCase))
            {
                var redisServerUri = configuration["DataProtectionKeysConfig:RedisServer"];

                using var connection = ConnectionMultiplexer.Connect(redisServerUri);

                _ = services.AddOpenTelemetryTracing(
                            builder =>
                            {
                                _ = builder.SetResourceBuilder(ResourceBuilder
                                       .CreateDefault()
                                       .AddService(webHostEnvironment.ApplicationName))
                                       .AddAspNetCoreInstrumentation((options) =>
                                       {
                                           // https://github.com/open-telemetry/opentelemetry-dotnet/tree/main/src/OpenTelemetry.Instrumentation.AspNetCore#filter
                                           // condition for allowable requests -> does not collect telemetry about the request if the Filter returns false or throws exception
                                           options.Filter = (httpContext) =>
                                           {
                                               bool isMetricsOrHealth = ValidateIsMetricsOrHealthRequest(httpContext.Request.Path);
                                               return !isMetricsOrHealth;
                                           };
                                       })
                                       .AddHttpClientInstrumentation(options => options.SetHttpFlavor = true)
                                       .AddRedisInstrumentation(connection, options => options.FlushInterval = TimeSpan.FromSeconds(5))
                                       .AddSqlClientInstrumentation(options =>
                                            {
                                                options.SetDbStatementForText = true;
                                                options.EnableConnectionLevelAttributes = true;
                                                options.RecordException = true;
                                            })
                                       //.AddOtlpExporter(options => options.Endpoint = new Uri(otlpReceiverUri))
                                       .AddJaegerExporter(o =>
                                       {
                                           o.AgentHost = "host.docker.internal";
                                           o.AgentPort = 6831;
                                       });

                                if (webHostEnvironment.IsDevelopment())
                                {
                                    builder.AddConsoleExporter(options => options.Targets = ConsoleExporterOutputTargets.Debug);
                                }
                            });
            }

            return services;
        }

        private static bool ValidateIsMetricsOrHealthRequest(PathString path) =>
            path.HasValue &&
            (path.Value.StartsWith("/metrics", StringComparison.InvariantCultureIgnoreCase) ||
             path.Value.StartsWith("/health", StringComparison.InvariantCultureIgnoreCase));
    }
}
