using Market.UseCases.Exceptions;
using System.Net;
using System.Text.Json;

namespace Market.Middleware
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
            catch (InvalidDataRequestException invalidDataEx)
            {
                Console.WriteLine(invalidDataEx.Message);
                await HandleInvalidDataRequestException(httpContext, invalidDataEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleInvalidDataRequestException(HttpContext context, InvalidDataRequestException invalidDataRequestEx)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = invalidDataRequestEx.Details.StatusCode;

            await context.Response.WriteAsJsonAsync(invalidDataRequestEx.Details);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            var errorDetails = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode
            };

            errorDetails.Messages.Add(exception.Message);

            await context.Response.WriteAsJsonAsync(errorDetails);
        }
    }
}
