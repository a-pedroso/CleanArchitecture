using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                ValidationException => ConfigResponse(context, HttpStatusCode.BadRequest, ex.Message, (ex as ValidationException).Errors),
                NotFoundException => ConfigResponse(context, HttpStatusCode.NotFound, ex.Message),
                _ => ConfigResponse(context, HttpStatusCode.InternalServerError, "Internal Server Error.")
            };

            return context.Response.WriteAsync(errorDetails.ToString());
        }

        private static ErrorDetails ConfigResponse(HttpContext context, HttpStatusCode statusCode, string message, IDictionary<string, string[]> errors = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Errors = errors
            };
        }
    }
}
