using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Middleware
{
    /// <summary>
    /// https://code-maze.com/global-error-handling-aspnetcore/
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ErrorDetails errorDetails = ex switch
            {
                ValidationException => ConfigResponse(context, ex.Message, HttpStatusCode.BadRequest),
                NotFoundException => ConfigResponse(context, ex.Message, HttpStatusCode.NotFound),
                _ => ConfigResponse(context, "Internal Server Error.", HttpStatusCode.InternalServerError)
            };

            return context.Response.WriteAsync(errorDetails.ToString());
        }

        private static ErrorDetails ConfigResponse(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };
        }
    }
}
