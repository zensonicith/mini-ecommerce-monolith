using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Settings;

namespace MyApp.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly JwtSetting _JwtSetting;
       

    public IdentityService(ICustomerRepository customerRepository, IOptions<JwtSetting> jwtSetting)
    {
        _customerRepository = customerRepository;
        _JwtSetting = jwtSetting.Value;
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
            Customer = (CustomerDto)customer,
            Token = GenerateToken(customer)
        };
    }

    private string GenerateToken(Customer customer)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtSetting.SecretKey));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim("username", customer.UserName),
            new Claim("id", customer.Id.ToString()),
            new Claim("role", customer.Role.RoleType.ToString()),
        };

        var token = new JwtSecurityToken(
            _JwtSetting.Issuer,
            _JwtSetting.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_JwtSetting.ExpireMinutes),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}