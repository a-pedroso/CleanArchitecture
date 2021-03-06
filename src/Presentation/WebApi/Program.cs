namespace CleanArchitecture.WebApi
{
    using CleanArchitecture.Infrastructure.Persistence.Context;
    using CleanArchitecture.WebApi.Helpers;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Prometheus.DotNetRuntime;
    using Serilog;
    using System;
    using System.IO;
    using System.Threading.Tasks;

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

            Console.WriteLine("Enabling prometheus-net.DotNetStats...");
            DotNetRuntimeStatsBuilder.Customize()
                .WithThreadPoolSchedulingStats()
                .WithContentionStats()
                .WithGcStats()
                .WithJitStats()
                .WithThreadPoolStats()
                .WithExceptionStats()
                .WithErrorHandler(ex => Log.Error(ex, "DotNetRuntime Error"))
                //.WithDebuggingMetrics(true);
                .StartCollecting();

            try
            {
                var host = CreateHostBuilder(args).Build();
                
                if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development")
                    && Configuration.GetValue<bool>("UseInMemoryDatabase") == false)
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
