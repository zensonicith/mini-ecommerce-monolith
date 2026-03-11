using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityService _identityService;
    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
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
}