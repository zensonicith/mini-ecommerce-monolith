namespace MyApp.Application.DTOs;

public class CreateCustomerRequest
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string UserName { get; set; }
    public string? City { get; set; }
    public string Password { get; set; }
}