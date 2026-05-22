namespace Shopping.Web.Service.Payments.Interfaces;

/// <summary>
/// Payment result containing the outcome of a payment operation.
/// </summary>
public class PaymentResult
{
    public bool IsSuccess { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

/// <summary>
/// Payment request containing details needed for processing a payment.
/// </summary>
public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string OrderId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
}

/// <summary>
/// Interface for payment processors.
/// </summary>
public interface IPaymentProcessor
{
    string PaymentType { get; }
    Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
    Task<PaymentResult> RefundPaymentAsync(string transactionId, decimal amount);
}
