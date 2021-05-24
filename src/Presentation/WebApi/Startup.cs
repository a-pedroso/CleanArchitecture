namespace CleanArchitecture.WebApi
{
    using CleanArchitecture.Application;
    using CleanArchitecture.Application.Common.Exceptions;
    using CleanArchitecture.Application.Common.Interfaces.Services;
    using CleanArchitecture.Infrastructure.Persistence;
    using CleanArchitecture.Infrastructure.Shared;
    using CleanArchitecture.WebApi.Extensions.StartupExtensions;
    using CleanArchitecture.WebApi.Services;
    using FluentValidation;
    using Hellang.Middleware.ProblemDetails;
    using Hellang.Middleware.ProblemDetails.Mvc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication()
                    .AddInfrastructurePersistence(Configuration)
                    .AddInfrastructureShared(Configuration);

            services.AddProblemDetails(x =>
            {
                x.Map<NotFoundException>(ex => new StatusCodeProblemDetails(StatusCodes.Status404NotFound));
                x.Map<ValidationException>(ex => new StatusCodeProblemDetails(StatusCodes.Status400BadRequest));
                x.Map<BadRequestException>(ex => new StatusCodeProblemDetails(StatusCodes.Status400BadRequest));
            });

            services.AddControllers()
                    .AddProblemDetailsConventions();

            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddMetricsExtension()
                    .AddOpenTelemetryExtension(Configuration, WebHostEnvironment)
                    .AddAuthenticationExtension(Configuration)
                    .AddDataProtectionKeysExtension(Configuration)
                    .AddForwardHeadersExtension(Configuration)
                    .AddHealthChecksExtension(Configuration)
                    .AddSwaggerExtension(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            app.UseForwardHeadersExtension(Configuration);

            app.UseSerilogRequestLogging();

            //app.UseExceptionMiddlewareExtension();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMetricsExtension();

            app.UseSwaggerExtension(Configuration);

            app.UseHealthChecksExtension();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                         .RequireAuthorization();
            });
        }
    }
}
