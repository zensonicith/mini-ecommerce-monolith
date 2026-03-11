namespace MyApp.Application.DTOs;

public class AuthenticationResultDto
{
    public bool IsAuthenticated { get; set; }
    public string Message { get; set; }
    public CustomerDto Customer { get; set; }
}