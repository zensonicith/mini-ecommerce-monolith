using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly ICustomerService _customerService;
    public AuthController(IIdentityService identityService, ICustomerService customerService)
    {
        _identityService = identityService;
        _customerService = customerService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticationDto authenticationDto)
    {
        var result = await _identityService.Authenticate(authenticationDto);
        if (!result.IsAuthenticated)
        {
            return Unauthorized(new { result.Message });
        }

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateCustomerRequest createCustomerRequest)
    {
        var customer = await _customerService.CreateAsync(createCustomerRequest);
        return Created($"/customer/{customer.Id}", customer);
    }
}