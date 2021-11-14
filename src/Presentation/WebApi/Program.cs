using CleanArchitecture.WebApi.Extensions;
using Serilog;

// serilog setup: https://github.com/datalust/dotnet6-serilog-example
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information("Starting up");

try
{
    ProgramExtensions.SetupMetrics();

    var builder = WebApplication.CreateBuilder(args);

    builder.SetupSerilog();

    // Add services to the container.
    builder.SetupServices();
 
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.SetupRequestPipeline();

    await app.SetupMigrationsAsync();

    Log.Information($"web api starting at {DateTime.UtcNow}");
    
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}