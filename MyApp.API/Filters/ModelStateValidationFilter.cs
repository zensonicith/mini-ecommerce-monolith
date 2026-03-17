using Microsoft.AspNetCore.Mvc.Filters;
using MyApp.Application.Exceptions;

namespace MyApp.API.Filters;

public sealed class ModelStateValidationFilter : IAsyncActionFilter
{
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ModelState.IsValid)
        {
            return next();
        }

        var errors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? (e.Exception?.Message ?? "Invalid value.") : e.ErrorMessage)
            .ToList();

        throw new ValidationException(errors);
    }
}

