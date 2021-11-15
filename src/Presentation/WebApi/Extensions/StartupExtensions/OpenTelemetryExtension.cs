namespace CleanArchitecture.WebApi.Extensions.StartupExtensions;

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
        if (!string.Equals(configuration["OpenTelemetryConfig:Enabled"], "true", StringComparison.OrdinalIgnoreCase))
        {
            return services;
        }

        string jaegerHost = configuration.GetValue<string>("OpenTelemetryConfig:JaegerExporter:AgentHost");
        int jaegerPort = configuration.GetValue<int>("OpenTelemetryConfig:JaegerExporter:AgentPort");

        return services.AddOpenTelemetryTracing(builder => builder
                                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(webHostEnvironment.ApplicationName))
                                .AddAspNetCoreInstrumentationExtension()
                                .AddHttpClientInstrumentation(options => options.SetHttpFlavor = true)
                                .AddRedisInstrumentationExtension(configuration)
                                .AddSqlClientInstrumentation(options =>
                                {
                                    options.SetDbStatementForText = true;
                                    options.EnableConnectionLevelAttributes = true;
                                    options.RecordException = true;
                                })
                                .AddConsoleExporterExtension(webHostEnvironment)
                                .AddJaegerExporter(o =>
                                {
                                    o.AgentHost = jaegerHost;
                                    o.AgentPort = jaegerPort;
                                }));
    }

    private static TracerProviderBuilder AddAspNetCoreInstrumentationExtension(this TracerProviderBuilder builder)
    {
        return builder.AddAspNetCoreInstrumentation((options) =>
        {
            // https://github.com/open-telemetry/opentelemetry-dotnet/tree/main/src/OpenTelemetry.Instrumentation.AspNetCore#filter
            // condition for allowable requests -> does not collect telemetry about the request if the Filter returns false or throws exception
            options.Filter = (httpContext) =>
            {
                bool isMetricsOrHealth = ValidateIsMetricsOrHealthRequest(httpContext.Request.Path);
                return !isMetricsOrHealth;
            };
        });

        static bool ValidateIsMetricsOrHealthRequest(PathString path) =>
            path.HasValue &&
            (path.Value.StartsWith("/metrics", StringComparison.InvariantCultureIgnoreCase) ||
             path.Value.StartsWith("/health", StringComparison.InvariantCultureIgnoreCase));
    }

    private static TracerProviderBuilder AddRedisInstrumentationExtension(this TracerProviderBuilder builder, IConfiguration configuration)
    {
        if (string.Equals(configuration["DataProtectionKeysConfig:Enabled"], "true", StringComparison.OrdinalIgnoreCase))
        {
            var redisServerUri = configuration.GetValue<string>("DataProtectionKeysConfig:RedisServer");
            using var connection = ConnectionMultiplexer.Connect(redisServerUri);

            builder.AddRedisInstrumentation(connection, options => options.FlushInterval = TimeSpan.FromSeconds(5));
        }

        return builder;
    }

    private static TracerProviderBuilder AddConsoleExporterExtension(this TracerProviderBuilder builder, IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.IsDevelopment())
        {
            builder.AddConsoleExporter(options => options.Targets = ConsoleExporterOutputTargets.Debug);
        }

        return builder;
    }
}
