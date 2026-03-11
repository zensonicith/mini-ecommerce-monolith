using MyApp.Domain.Entities;

namespace MyApp.Application.DTOs;

public class AuthenticationDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}