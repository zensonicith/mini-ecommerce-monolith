namespace MyApp.Domain.Enum;

public enum EOrderStatus
{
    Pending,
    PaymentProcessing,
    Paid,
    Failed,
    Cancelled,
    Shipped,
    Completed
}