using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(
    ICustomerService customerService
    ) : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCustomerProfile()
    {
        var username = User.FindFirstValue("username");
        var customer = await customerService.GetByUserNameAsync(username);
        return Ok(customer);
    }
}