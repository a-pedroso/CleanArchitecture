using CleanArchitecture.WebApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    public static class SwaggerExtension
    {
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
                            Name = "DPS",
                            Url = new Uri("https://www.ceiia.com/"),
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
    }
}