using MyApp.Domain.Entities;

namespace MyApp.Application.DTOs;

public class OrderResponseDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; }

    public decimal TotalAmount { get; set; }
    public string Currency { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }

    public string ShippingName { get; set; }
    public string ShippingPhone { get; set; }
    public string ShippingAddressLine { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingCountry { get; set; }
    public string ShippingPostalCode { get; set; }

    public List<OrderItemResponseDto> Items { get; set; }
    
    public static explicit operator OrderResponseDto(Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,

            TotalAmount = order.TotalAmount,
            Currency = order.Currency,

            Status = order.Status.ToString(),

            CreatedAt = order.CreatedAt,
            PaidAt = order.PaidAt,

            ShippingName = order.ShippingName,
            ShippingPhone = order.ShippingPhone,
            ShippingAddressLine = order.ShippingAddressLine,
            ShippingCity = order.ShippingCity,
            ShippingCountry = order.ShippingCountry,
            ShippingPostalCode = order.ShippingPostalCode,

            Items = order.OrderProducts
                .Select(x => (OrderItemResponseDto)x)
                .ToList()
        };
    }
}
