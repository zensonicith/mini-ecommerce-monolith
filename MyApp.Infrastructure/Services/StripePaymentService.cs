using MyApp.Application.DTOs;
using MyApp.Application.Exceptions;
using MyApp.Application.Interfaces;
using MyApp.Domain.Enum;
using Stripe.Checkout;

namespace MyApp.Infrastructure.Services;

public class StripePaymentService : IPaymentService
{
    private readonly IOrderService _orderService;

    private const string FrontendUrl = "http://localhost:4200";

    public StripePaymentService(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<string> CreateCheckoutSessionAsync(int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);

        if (order.Items.Count == 0)
            throw new ValidationException("Order has no items");

        var options = new SessionCreateOptions
        {
            Mode = "payment",
            SuccessUrl =
                $"{FrontendUrl}/checkout/success?session_id={{CHECKOUT_SESSION_ID}}",

            CancelUrl =
                $"{FrontendUrl}/checkout/cancel",

            Metadata = new Dictionary<string, string>
            {
                { "order_id", orderId.ToString() }
            },

            LineItems = order.Items.Select(item =>
                new SessionLineItemOptions
                {
                    Quantity = item.Quantity,

                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = order.Currency,
                        UnitAmountDecimal = item.UnitPrice,

                        ProductData =
                            new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.ProductName
                            }
                    }
                }).ToList()
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Url;
    }

    public async Task<OrderResponseDto> VerifySessionAsync(string sessionId)
    {
        var stripe = new SessionService();

        var session = await stripe.GetAsync(sessionId);

        if (session.PaymentStatus != "paid")
            throw new ValidationException("Payment not completed");

        var orderId =
            int.Parse(session.Metadata["order_id"]);

        var order =
            await _orderService.GetDomainOrderByIdAsync(orderId);

        if (order.Status != EOrderStatus.Paid)
        {
            order.Status = EOrderStatus.Paid;
            order.PaidAt = DateTime.UtcNow;
            order.StripePaymentIntentId = session.PaymentIntentId;

            await _orderService.UpdateDomainOrderAsync(order);
        }

        return (OrderResponseDto)order;
    }
}