namespace MyApp.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string entityName, int id) : base($"{entityName} with id {id} was not found.")
    {
    }
    
    public NotFoundException(string entityName, string username) : base($"{entityName} with username {username} was not found.")
    {
    }
}