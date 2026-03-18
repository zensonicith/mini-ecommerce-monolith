namespace MyApp.Application.DTOs;

public class UpdateOrderRequestDto
{
    public string? ShippingName { get; set; }
    public string? ShippingPhone { get; set; }
    public string? ShippingAddressLine { get; set; }
    public string? ShippingCity { get; set; }
    public string? ShippingCountry { get; set; }
    public string? ShippingPostalCode { get; set; }

    public List<UpdateOrderItemDto> Items { get; set; } = [];
}