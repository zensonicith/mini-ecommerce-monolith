using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> GetByUserNameAsync(string userName);
    Task<bool> ExistsByUserNameAsync(string userName);
    Task<Customer> AddAsync(Customer customer);
}