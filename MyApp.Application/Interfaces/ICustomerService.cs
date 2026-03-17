using MyApp.Application.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerDto?> GetByUserNameAsync(string userName);
    Task<CustomerDto?> CreateAsync(CreateCustomerRequest createCustomerRequest);
}