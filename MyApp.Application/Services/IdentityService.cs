using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly ICustomerRepository _customerRepository;
    public IdentityService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<AuthenticationResultDto> Authenticate(AuthenticationDto authenticationDto)
    {
        var customer = await _customerRepository.GetByUserNameAsync(authenticationDto.UserName);
        if (customer == null || !customer.Password.Equals(authenticationDto.Password))
        {
            return new AuthenticationResultDto
            {
                IsAuthenticated = false,
                Message = "Invalid credentials"
            };
        }

        return new AuthenticationResultDto
        {
            IsAuthenticated = true,
            Customer = (CustomerDto)customer
        };
    }
}