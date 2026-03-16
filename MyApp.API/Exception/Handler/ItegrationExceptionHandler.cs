using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.API.Exception.Handler;

public class IntegrationExceptionHandler(ILogger<IntegrationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not UnauthorizedAccessException) return false;

        httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
        logger.LogWarning(exception, "An external server error occurred.");
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status502BadGateway,
            Title = "External Server Error",
            Detail = exception.Message
        }, cancellationToken);

        return true;
    }
}