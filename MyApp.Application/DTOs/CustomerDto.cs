using System.Text.Json.Serialization;
using MyApp.Domain.Entities;
using MyApp.Domain.Enum;

namespace MyApp.Application.DTOs;

public class CustomerDto
{ 
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string UserName { get; set; }
    public string? City { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ERole? Role { get; set; }
    
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
            Id = customer.Id,
            Role = customer.Role?.RoleType
        };
    }
}