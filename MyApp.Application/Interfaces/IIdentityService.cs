using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces;
public interface IIdentityService
{
    Task<AuthenticationResultDto> Authenticate(AuthenticationDto authenticationDto);
}