using Finora.Application.Exceptions;
using FluentValidation;

namespace Finora.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                context.Response.ContentType = "application/json";

                if (ex is ValidationException validationException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var errors = validationException.Errors.GroupBy(x => x.PropertyName).ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = 400,
                        Message = "Validation failed.",
                        Errors = errors
                    });

                    return;
                }
                var statusCode = ex switch
                {
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    ConflictException => StatusCodes.Status409Conflict,
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = statusCode,
                    Message = ex.Message
                });
            }
        }
    }
}
