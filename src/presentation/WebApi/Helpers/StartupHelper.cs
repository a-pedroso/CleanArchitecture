using CleanArchitecture.WebApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.IO;
using System.Reflection;

namespace CleanArchitecture.WebApi.Helpers
{
    public static class StartupHelper
    {
        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio#customize-and-extend
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = ApiConfigurationConsts.ApiName,
                        Version = ApiConfigurationConsts.ApiVersionV1,
                        Contact = new OpenApiContact
                        {
                            Name = "André Pedroso",
                            Email = "andre.pedroso@outlook.com",
                            Url = new Uri("https://a-pedroso.github.io"),
                        }
                    });

                // Set the comments path for the Swagger JSON and UI - previously generated XML - csproj XML -> GenerateDocumentationFile
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiConfigurationConsts.ApiName);
            });
            return app;
        }

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

        public static IServiceCollection AddDataProtectionKeysExtension(this IServiceCollection services, IConfiguration configuration)
        {
            if (string.Equals(configuration["DataProtectionKeysConfig:Enabled"], "true", StringComparison.OrdinalIgnoreCase))
            {
                // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/implementation/key-storage-providers?view=aspnetcore-5.0&tabs=visual-studio#redis

                var redisServerUri = configuration["DataProtectionKeysConfig:RedisServer"];

                var redis = ConnectionMultiplexer.Connect(redisServerUri);

                services.AddDataProtection()
                        .PersistKeysToStackExchangeRedis(redis, "Clean-Architecture-DataProtection-Keys");
            }

            return services;
        }

    }
}
