using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    public static class HealthChecksExtension
    {
        public static IServiceCollection AddHealthChecksExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddSqlServer(configuration.GetConnectionString("DefaultConnection"));

            //TODO: add health check dependencies

            return services;
        }
    }
}