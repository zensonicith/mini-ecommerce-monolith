using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces.Repo;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence;

namespace MyApp.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _dbContext;

    public CartRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Cart?> GetByIdAsync(int id)
    {
        return await _dbContext.Carts
            .Include(cart => cart.Product)
            .Include(cart => cart.Customer)
            .FirstOrDefaultAsync(cart => cart.Id == id);
    }

    public async Task<List<Cart>> GetAllCartsByUserIdAsync(int userId)
    {
        return await _dbContext.Carts
            .AsNoTracking()
            .Include(cart => cart.Product)
            .Include(cart => cart.Customer)
            .Where(cart => cart.CustomerId == userId)
            .ToListAsync();
    }

    public async Task<Cart> AddCartsAsync(Cart cart)
    {
        await _dbContext.Carts.AddAsync(cart);
        await _dbContext.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart> UpdateCartAsync(Cart cart)
    {
        _dbContext.Carts.Update(cart);
        await _dbContext.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart> RemoveCartAsync(Cart cart)
    {
        _dbContext.Carts.Remove(cart);
        await _dbContext.SaveChangesAsync();
        return cart;
    }
}