using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence;

namespace MyApp.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;

    public CustomerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Customer> GetByUserNameAsync(string userName)
    {
        var user = await _dbContext.Customers
            .Include(c => c.Role)
            .SingleOrDefaultAsync(customer => customer.UserName.Equals(userName));
        return user;
    }
}