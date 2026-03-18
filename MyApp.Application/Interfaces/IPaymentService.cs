using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces;

public interface IPaymentService
{
    Task<string> CreateCheckoutSessionAsync(int orderId);
    
    Task<OrderResponseDto> VerifySessionAsync(string sessionId);
}