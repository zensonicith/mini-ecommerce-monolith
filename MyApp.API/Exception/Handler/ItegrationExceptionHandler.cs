using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Exceptions;

namespace MyApp.API.Exception.Handler;

public class IntegrationExceptionHandler(ILogger<IntegrationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not IntegrationException) return false;

        httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
        logger.LogWarning(exception, "An integration error occurred.");

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status502BadGateway,
            Title = "Bad Gateway",
            Detail = exception.Message,
            Extensions = { ["traceId"] = httpContext.TraceIdentifier }
        }, cancellationToken);

        return true;
    }
}
