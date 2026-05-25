using Shopping.Web.Service.Payments.Interfaces;

namespace Shopping.Web.Service.Payments.Factory;

/// <summary>
/// Cash on Delivery payment processor — no online charge; order is confirmed on delivery.
/// </summary>
public class CashOnDeliveryPaymentProcessor : IPaymentProcessor
{
    public string PaymentType => "CashOnDelivery";

    public Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        return Task.FromResult(new PaymentResult
        {
            IsSuccess = true,
            TransactionId = $"COD-{Guid.NewGuid()}",
            Message = "Cash on Delivery order confirmed.",
            Amount = request.Amount
        });
    }

    public Task<PaymentResult> RefundPaymentAsync(string transactionId, decimal amount)
    {
        return Task.FromResult(new PaymentResult
        {
            IsSuccess = true,
            TransactionId = transactionId,
            Message = "Cash on Delivery refund processed.",
            Amount = amount
        });
    }
}
