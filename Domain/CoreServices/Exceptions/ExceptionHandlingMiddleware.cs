using Domain.CoreServices;
using Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Domain.Services.Exceptions
{
        public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment environment) : IMiddleware
        {
            public async Task InvokeAsync(HttpContext context, RequestDelegate next)
            {
                try
                {
                    await next(context);
                }
                catch (ValidationException ex)
                {
                    var message = string.Join(", ", ex.Errors
                        .Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));

                       await WriteErrorResponseAsync(context, message, environment.IsDevelopment() ? ex.StackTrace : null);

                    logger.LogError(ex, "Validation error occurred: {Message}", message);
                }
                catch (Exception ex)
                {
                    await WriteErrorResponseAsync(context, ex.Message, environment.IsDevelopment() ? ex.StackTrace : null);
                
                    logger.LogError(ex, "Validation error occurred: {Message}", ex.Message);

                }
        }

            private async Task WriteErrorResponseAsync(HttpContext context, string message, string? details)
            {
                var operationResult = new OperationResult<object>
                {
                    StatusCode = Statuses.Failed,
                    Message = message,
                    ExceptionDetails = details,
                };

               
                var json = JsonSerializer.Serialize(operationResult);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
}
