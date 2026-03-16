using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.API.Exception.Handler;

public class ForbiddenExceptionHandler(ILogger<ForbiddenExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not UnauthorizedAccessException) return false;

        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        logger.LogWarning(exception, "A forbidden access error occurred.");
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden Error",
            Detail = exception.Message
        }, cancellationToken);

        return true;
    }
}