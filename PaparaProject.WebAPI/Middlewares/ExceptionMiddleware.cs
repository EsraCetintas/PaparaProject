using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System;
using System.Threading.Tasks;
using PaparaProject.Application.Utilities.Results;
using Newtonsoft.Json;
using System.Net.Http;
using FluentValidation;

namespace PaparaProject.WebAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
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
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (context.Response.StatusCode != 401)
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception.GetType() == typeof(ValidationException))
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

            context.Response.ContentType = "application/json";         
            var json = JsonConvert.SerializeObject(new APIResult()
            {
                Message = exception.Message,
                Success = false,
                Data = exception.Data.ToString()

            });

          return context.Response.WriteAsync(json);
         }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
