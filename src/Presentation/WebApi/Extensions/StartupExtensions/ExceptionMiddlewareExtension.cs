using CleanArchitecture.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CleanArchitecture.WebApi.Extensions.StartupExtensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseExceptionMiddlewareExtension(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}