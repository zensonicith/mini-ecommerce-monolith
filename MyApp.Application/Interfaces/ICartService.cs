using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces;

public interface ICartService
{
    Task<List<CartResponseDto>> GetAllCartsByUserIdAsync(int userId);
    Task<CartResponseDto> AddCartAsync(int userId, CartRequestDto request);
    Task<bool> UpdateCartAsync(int id, int userId, CartRequestDto request);
    Task<bool> RemoveCartAsync(int id, int userId);
}
