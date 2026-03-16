namespace MyApp.Application.Exceptions;

public class UnauthorizedException : ApplicationException
{
    public  UnauthorizedException() : base("Authentication required. Please log in and try again.")
    {
    }
}