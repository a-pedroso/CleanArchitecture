//namespace CleanArchitecture.WebApi.Middleware
//{
//    using CleanArchitecture.Application.Common.Exceptions;
//    using Microsoft.AspNetCore.Http;
//    using Microsoft.AspNetCore.Mvc;
//    using Microsoft.Extensions.Logging;
//    using System;
//    using System.Net;
//    using System.Threading.Tasks;

//    public class ExceptionMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<ExceptionMiddleware> _logger;
//        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
//        {
//            _logger = logger;
//            _next = next;
//        }
//        public async Task InvokeAsync(HttpContext httpContext)
//        {
//            try
//            {
//                await _next(httpContext);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Something went wrong: {ex}");
//                //HandleExceptionAsync(ex);

//                if(ex is BadRequestException)
//                {
//                    throw new BadRequestException(ex.Message);
//                }

//                if (ex is ValidationException)
//                {
//                    throw new BadRequestException(ex.Message);
//                }

//                if (ex is NotFoundException)
//                {
//                    throw new NotFoundException(ex.Message);
//                }

//                throw;
//            }
//        }
//        //private static void HandleExceptionAsync(Exception ex)
//        //{
//        //    _ = ex switch
//        //    {
//        //        BadRequestException => throw new BadRequestException(ex.Message),
//        //        ValidationException => throw new BadRequestException(ex.Message),  //ConfigResponse(context, HttpStatusCode.BadRequest, ex.Message, (ex as ValidationException).Errors),
//        //        NotFoundException => throw new NotFoundException(ex.Message), //ConfigResponse(context, HttpStatusCode.NotFound, ex.Message),
//        //        _ => throw ex // ConfigResponse(context, HttpStatusCode.InternalServerError, "Internal Server Error.")
//        //    };
//        //}
//    }
//}


////namespace CleanArchitecture.WebApi.Middleware
////{
////    using CleanArchitecture.Application.Common.Exceptions;
////    using CleanArchitecture.WebApi.Models;
////    using Microsoft.AspNetCore.Http;
////    using Microsoft.AspNetCore.Mvc;
////    using Microsoft.Extensions.Logging;
////    using System;
////    using System.Collections.Generic;
////    using System.Net;
////    using System.Threading.Tasks;

////    /// <summary>
////    /// https://code-maze.com/global-error-handling-aspnetcore/
////    /// </summary>
////    public class ExceptionMiddleware
////    {
////        private readonly RequestDelegate _next;
////        private readonly ILogger<ExceptionMiddleware> _logger;
////        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
////        {
////            _logger = logger;
////            _next = next;
////        }
////        public async Task InvokeAsync(HttpContext httpContext)
////        {
////            try
////            {
////                await _next(httpContext);
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError($"Something went wrong: {ex}");
////                await HandleExceptionAsync(httpContext, ex);
////            }
////        }
////        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
////        {
////            ProblemDetails errorDetails = ex switch
////            {
////                BadRequestException => ConfigResponse(context, HttpStatusCode.BadRequest, ex.Message),
////                ValidationException => ConfigResponse(context, HttpStatusCode.BadRequest, ex.Message, (ex as ValidationException).Errors),
////                NotFoundException => ConfigResponse(context, HttpStatusCode.NotFound, ex.Message),
////                _ => ConfigResponse(context, HttpStatusCode.InternalServerError, "Internal Server Error.")
////            };

////            await context.Response.WriteAsync(errorDetails.ToString());
////        }

////        private static ProblemDetails ConfigResponse(HttpContext context, HttpStatusCode statusCode, string message, IDictionary<string, string[]> errors = null)
////        {
////            context.Response.ContentType = "application/json";
////            context.Response.StatusCode = (int)statusCode;

////            return new ProblemDetails()
////            {
////                Status = context.Response.StatusCode,
////                Title = message


////                //StatusCode = context.Response.StatusCode,
////                //Message = message,
////                //Errors = errors
////            };
////        }
////    }
////}
