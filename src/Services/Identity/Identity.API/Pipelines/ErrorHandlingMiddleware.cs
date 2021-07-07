using Identity.API.Helpers;
using Identity.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Pipelines
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = context.Response.StatusCode;

            try
            {
                if (exception != null && exception is HttpException)
                {
                    statusCode = (exception as HttpException).StatusCode;
                }
                else
                {
                    if (exception != null)
                    {
                        statusCode = 500;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                string jsonError = default;

                if (statusCode == 500)
                {
                    ErrorModel errorMessage = new ErrorModel
                    {
                        Key = "internal_error"
                    };

                    jsonError = JsonConvert.SerializeObject(errorMessage);
                }
                else
                {
                    jsonError = exception.Message;
                }

                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(jsonError);
            }
        }


    }
}
