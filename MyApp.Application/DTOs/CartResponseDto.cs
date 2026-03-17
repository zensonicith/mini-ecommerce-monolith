using MyApp.Domain.Entities;

namespace MyApp.Application.DTOs;

public class CartResponseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? UserName { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }

    public static explicit operator CartResponseDto(Cart cart)
    {
        if (cart == null)
            return null;

        return new CartResponseDto
        {
            Id = cart.Id,
            CustomerId = cart.CustomerId,
            CustomerName = cart.Customer?.Name,
            UserName = cart.Customer?.UserName,
            ProductId = cart.ProductId,
            ProductName = cart.Product?.ProductName,
            Price = cart.Product?.Price ?? 0,
            ImageUrl = cart.Product?.ImageUrl
        };
    }
}
