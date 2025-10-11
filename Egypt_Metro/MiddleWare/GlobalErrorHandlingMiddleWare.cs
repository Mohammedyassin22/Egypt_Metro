using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.ErrorModels;
using Shared.ErrorModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_Metro.MiddleWare
{
    public class GlobalErrorHandlingMiddleWare
    {
        private readonly ILogger<GlobalErrorHandlingMiddleWare> _logger;
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleWare(ILogger<GlobalErrorHandlingMiddleWare> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                await HandleNotFoundAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new ErrorDetails
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message
            };

            switch (ex)
            {
                case UnauthorizedAccessException:
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.ErrorMessage = "Unauthorized access";
                    break;

                case ValidationException vex:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ErrorMessage = string.Join(" | ", vex.Errors);
                    break;

                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.ErrorMessage = ex.Message;
                    break;
            }

            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task HandleNotFoundAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Resource not found"
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
