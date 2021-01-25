using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    public static class ForwardHeadersExtension
    {
        public static IServiceCollection AddForwardHeadersExtension(this IServiceCollection services, IConfiguration configuration)
        {
            if (string.Equals(configuration["ForwardHeadersEnabled"], "true", StringComparison.OrdinalIgnoreCase))
            {
                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.All;
                    // this clear is needed because this app is hosted behind a reverse proxy.
                    // why you may ask... 
                    // because if you dont clear this KnownNetworks and KnownProxies, 
                    // the forward header XForwardedProto is not forward at all
                    // https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-2.2#troubleshoot
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                });
            }

            return services;
        }

        public static IApplicationBuilder UseForwardHeadersExtension(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (string.Equals(configuration["ForwardHeadersEnabled"], "true", StringComparison.OrdinalIgnoreCase))
            {
                app.UseForwardedHeaders();
            }

            return app;
        }

    }
}