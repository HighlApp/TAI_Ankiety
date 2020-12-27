using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Surveys.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
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
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            List<string> messages = new List<string>();
            string exceptionMessage = "Unexpected error";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            Type exceptionType = exception.GetType();

            switch (exception)
            {
                case { } e when exceptionType == typeof(JwtTokenException):
                    statusCode = HttpStatusCode.Unauthorized;
                    messages.Add(exceptionMessage);
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            object response;

            if (messages.Count > 0)
            {
                response = new { 
                    StatusCode = (int)statusCode, 
                    Message = messages 
                };
            }
            else
            {
                response = new { 
                    StatusCode = (int)statusCode 
                };
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            string payload = JsonConvert.SerializeObject(response);
            await context.Response.WriteAsync(payload);
        }
    }
}
