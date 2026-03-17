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

    public async Task<CustomerDto?> CreateAsync(CreateCustomerRequest createCustomerRequest)
    {
        if (await _customerRepository.ExistsByUserNameAsync(createCustomerRequest.UserName))
        {
            throw new ConflictException("Username is already used");
        }

        Customer customer = new Customer
        {
            Address = createCustomerRequest.Address,
            City = createCustomerRequest.City,
            Name = createCustomerRequest.Name,
            UserName = createCustomerRequest.UserName,
            Password = createCustomerRequest.Password,
            RoleId = 2
        };
        await _customerRepository.AddAsync(customer);
        var customerDto = (CustomerDto)customer;
        return customerDto;
    }
}