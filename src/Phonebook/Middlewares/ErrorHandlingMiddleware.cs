using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Phonebook.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Phonebook.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = ex switch
            {
                NotFoundException _ => HttpStatusCode.NotFound,
                BadRequestException _ => HttpStatusCode.BadRequest,
                UnauthorizedException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
