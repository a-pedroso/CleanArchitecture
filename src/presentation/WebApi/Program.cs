using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Json;
using App.Metrics.Formatters.Prometheus;
using CleanArchitecture.Infrastructure.Persistence.Context;
using CleanArchitecture.WebApi.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi
{

    public class Program
    {
        // serilog setup
        // https://nblumhardt.com/2019/10/serilog-in-aspnetcore-3/
        // https://github.com/serilog/serilog-aspnetcore/blob/71165692d5f66c811c3b251047b12c259ac2fe23/samples/EarlyInitializationSample/Program.cs#L12

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureMetricsWithDefaults(builder =>
                {
                    builder.OutputMetrics.AsPrometheusPlainText();
                    //builder.Report.ToConsole(TimeSpan.FromSeconds(5));
                    //builder.Report.ToTextFile(
                    //    options => {
                    //        options.MetricsOutputFormatter = new MetricsJsonOutputFormatter();
                    //        options.AppendMetricsToTextFile = false;
                    //        options.FlushInterval = TimeSpan.FromSeconds(5);
                    //        options.OutputPathAndFileName = @"Logs\metrics.txt";
                    //    });
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
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Debug()
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();
                
                if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
                {
                    using var serviceScope = host.Services.CreateScope();
                    
                    var services = serviceScope.ServiceProvider;
                    using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                    await DbMigrationHelper<ApplicationDbContext>.EnsureDatabaseMigratedAsync(scope); 
                }

                Log.Information($"web api starting at {DateTime.UtcNow}");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
