using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public CheckoutController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("create-session")]
    public async Task<IActionResult> CreateSession(
        [FromBody] CreateSessionRequestDto request)
    {
        var url =
            await _paymentService.CreateCheckoutSessionAsync(request.OrderId);

        return Ok(new { url });
    }

    [HttpGet("verify-session")]
    public async Task<ActionResult<OrderResponseDto>>
        VerifySession([FromQuery] string sessionId)
    {
        var result =
            await _paymentService.VerifySessionAsync(sessionId);

        return Ok(result);
    }
}