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

    public async Task<bool> ExistsByUserNameAsync(string userName)
    {
        var isExisted = await _dbContext.Customers.AnyAsync(customer => customer.UserName.Equals(userName));
        return isExisted;
    }

    public async Task<Customer> AddAsync(Customer customer)
    { 
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
        return customer;
    }
}