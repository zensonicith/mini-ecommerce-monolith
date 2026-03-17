using MyApp.Domain.Enum;

namespace MyApp.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public ICollection<OrderDetails> OrderProducts { get; set; } = new List<OrderDetails>();

        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "usd";

        public EOrderStatus Status { get; set; }

        public string? StripePaymentIntentId { get; set; }

        // Snapshot
        public string? OrderEmail { get; set; }
        public string? ShippingName { get; set; }
        public string? ShippingPhone { get; set; }
        public string? ShippingAddressLine { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingCountry { get; set; }
        public string? ShippingPostalCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }
    }
}