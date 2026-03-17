using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces.Repo;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(int id);
    Task<List<Cart>> GetAllCartsByUserIdAsync(int userId);
    Task<Cart> AddCartsAsync(Cart cart);
    Task<Cart> UpdateCartAsync(Cart cart);
    Task<Cart> RemoveCartAsync(Cart cart);
}