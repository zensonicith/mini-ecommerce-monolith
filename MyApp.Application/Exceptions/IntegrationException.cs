namespace MyApp.Application.Exceptions;

public class IntegrationException : ApplicationException
{
    public  IntegrationException(string message) : base(message)
    {
    }
}