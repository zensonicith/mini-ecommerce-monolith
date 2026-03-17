using MyApp.Domain.Entities;

namespace MyApp.Application.DTOs;

public class OrderItemResponseDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal => Quantity * UnitPrice;

    public static explicit operator OrderItemResponseDto(OrderDetails orderDetails)
    {
        return new OrderItemResponseDto
        {
            ProductId = orderDetails.ProductId,
            ProductName = orderDetails.Product.ProductName,
            Quantity = orderDetails.Quantity,
            UnitPrice = orderDetails.UnitPrice
        };
    }
}