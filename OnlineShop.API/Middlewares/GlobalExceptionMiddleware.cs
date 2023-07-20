using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.Common.ResponseModel;
using Serilog;
using System.Net;

namespace OnlineShop.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {

                await HandleException(httpContext, ex, HttpStatusCode.NotFound, ex.Message);
            }
            catch (AlreadyExistsException ex)
            {
                await HandleException(httpContext, ex, HttpStatusCode.Conflict, ex.Message);
            }
            catch (ValidationException ex)
            {
                await HandleException(httpContext, ex, HttpStatusCode.BadRequest, ex.Message);
            }

            catch (Exception ex)
            {
                await HandleException(httpContext, ex, HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        public async ValueTask<ActionResult> HandleException<TException>(HttpContext httpContext, TException ex, HttpStatusCode httpStatusCode, string message)
        {

            HttpResponse response = httpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            ResponseCore<TException> error = new()
            {
                Errors = new string[] { message },
                StatusCode = httpStatusCode,
                IsSuccess = false,
                Result = ex
            };

            return await Task.FromResult(new BadRequestObjectResult(error));

        }
    }


    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }

}

