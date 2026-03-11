using MyApp.Domain.Entities;

namespace MyApp.Application.DTOs;

public class CustomerDto
{ 
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string UserName { get; set; }
    public string? City { get; set; }
    
    public static explicit operator CustomerDto(Customer customer)
    {
        if (customer == null)
            return null;
        return new CustomerDto
        {
            UserName = customer.UserName,
            Address = customer.Address,
            City = customer.City,
            Name = customer.Name,
            Id = customer.Id
        };
    }
}