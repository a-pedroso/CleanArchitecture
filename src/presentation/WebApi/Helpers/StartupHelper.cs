using CleanArchitecture.WebApi.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CleanArchitecture.WebApi.Helpers
{
    public static class StartupHelper
    {
        public static IServiceCollection AddAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = configuration.GetValue<string>("Authentication:Jwt:Authority");
                options.RequireHttpsMetadata = configuration.GetValue<bool>("Authentication:Jwt:RequireHttpsMetadata");
                options.Audience = configuration.GetValue<string>("Authentication:Jwt:Audience");
            });

            return services;
        }


        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var oauthAuthority = configuration.GetValue<string>("Authentication:Jwt:Authority");
            var oauthDefinition = "oauth2";
            var oauthScopes = new Dictionary<string, string>
            {
                { configuration.GetValue<string>("Authentication:Swagger:Scopes:Api"), ApiConfigurationConsts.ApiName }
            };


            // https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio#customize-and-extend
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    ApiConfigurationConsts.ApiVersionV1,
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

                options.AddSecurityDefinition(
                    oauthDefinition,
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            ClientCredentials = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri($"{oauthAuthority}/connect/authorize"),
                                TokenUrl = new Uri($"{oauthAuthority}/connect/token"),
                                Scopes = oauthScopes
                            }
                        }
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = oauthDefinition
                            }
                        },
                        oauthScopes.Keys.ToArray()
                    } });

            });
        }

        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId(configuration.GetValue<string>("Authentication:Swagger:Client"));
                options.SwaggerEndpoint("/swagger/v1/swagger.json", ApiConfigurationConsts.ApiName);
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
