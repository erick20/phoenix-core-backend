using Identity.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Identity.API.Pipelines
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = context.Response.StatusCode;
            string jsonError = default;
            try
            {
                if (exception != null && exception is HttpException)
                {
                    statusCode = (exception as HttpException).StatusCode;
                    jsonError = exception.Message;
                }
                else
                {
                    if (exception != null)
                    {
                        ErrorModel errorMessage = new ErrorModel
                        {
                            Key = "internal_error"
                        };

                        statusCode = 500;
                        jsonError = JsonConvert.SerializeObject(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(jsonError);
            }
        }


    }
}
