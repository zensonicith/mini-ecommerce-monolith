namespace MyApp.Application.Exceptions;

public class IntegrationException : ApplicationException
{
    public IntegrationException(string message) : base(message)
    {
    }

    public IntegrationException(string message, Exception inner) : base(message, inner)
    {
    }
}
