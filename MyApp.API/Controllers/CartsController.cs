using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Exceptions;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartsController(ICartService cartService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMyCarts()
    {
        var userId = GetCurrentUserId();
        var carts = await cartService.GetAllCartsByUserIdAsync(userId);
        return Ok(carts);
    }

    [HttpPost]
    public async Task<IActionResult> AddCart([FromBody] CartRequestDto request)
    {
        var userId = GetCurrentUserId();
        var cart = await cartService.AddCartAsync(userId, request);
        return Created($"/api/carts/{cart.Id}", cart);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCart([FromRoute] int id, [FromBody] CartRequestDto request)
    {
        var userId = GetCurrentUserId();
        var success = await cartService.UpdateCartAsync(id, userId, request);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveCart([FromRoute] int id)
    {
        var userId = GetCurrentUserId();
        var success = await cartService.RemoveCartAsync(id, userId);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    private int GetCurrentUserId()
    {
        var userId = User.FindFirstValue("id");

        if (!int.TryParse(userId, out var parsedUserId))
        {
            throw new UnauthorizedException();
        }

        return parsedUserId;
    }
}