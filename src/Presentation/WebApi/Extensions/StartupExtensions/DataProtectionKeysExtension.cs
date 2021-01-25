using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;

namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    public static class DataProtectionKeysExtension
    {
        public static IServiceCollection AddDataProtectionKeysExtension(this IServiceCollection services, IConfiguration configuration)
        {
            if (string.Equals(configuration["DataProtectionKeysConfig:Enabled"], "true", StringComparison.OrdinalIgnoreCase))
            {
                // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/implementation/key-storage-providers?view=aspnetcore-5.0&tabs=visual-studio#redis

                var redisServerUri = configuration["DataProtectionKeysConfig:RedisServer"];

                var redis = ConnectionMultiplexer.Connect(redisServerUri);

                services.AddDataProtection()
                        .PersistKeysToStackExchangeRedis(redis, "Firmware-Manager-DataProtection-Keys");
            }

            return services;
        }
    }
}