namespace MyApp.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException(string message) : base(message)
    {
    }

    public ValidationException(IEnumerable<string> errors)
        : base("One or more validation failures occurred.")
    {
        Errors = errors.ToList();
    }

    public List<string> Errors { get; } = [];
}