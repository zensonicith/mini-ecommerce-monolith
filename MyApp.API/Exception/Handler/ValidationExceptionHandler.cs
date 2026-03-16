using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Exceptions;

namespace MyApp.API.Exception.Handler;

public class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException) return false;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        logger.LogWarning(exception, "A bad request error occurred.");
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request Error",
            Detail = exception.Message
        }, cancellationToken);

        return true;
    }
}