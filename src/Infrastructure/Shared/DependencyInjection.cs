namespace CleanArchitecture.Infrastructure.Shared
{
    using CleanArchitecture.Application.Common.Interfaces.Services;
    using CleanArchitecture.Infrastructure.Shared.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
