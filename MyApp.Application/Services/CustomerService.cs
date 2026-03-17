using MyApp.Application.DTOs;
using MyApp.Application.Exceptions;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<CustomerDto?> GetByUserNameAsync(string userName)
    {
        var customer = await _customerRepository.GetByUserNameAsync(userName);

        if (customer == null)
            throw new NotFoundException(nameof(customer), customer.UserName);
        
        return (CustomerDto)customer;
    }
}