using Shopping.Web.Service.Payments.Interfaces;

namespace Shopping.Web.Service.Payments.Factory;

/// <summary>
/// Credit Card payment processor implementation.
/// </summary>
public class CreditCardPaymentProcessor : IPaymentProcessor
{
    public string PaymentType => "CreditCard";

    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        // TODO: Implement actual credit card payment processing logic
        // This would integrate with payment gateway APIs
        
        await Task.Delay(100); // Simulating API call
        
        return new PaymentResult
        {
            IsSuccess = true,
            TransactionId = Guid.NewGuid().ToString(),
            Message = "Credit card payment processed successfully.",
            Amount = request.Amount
        };
    }

    public async Task<PaymentResult> RefundPaymentAsync(string transactionId, decimal amount)
    {
        // TODO: Implement actual refund logic
        
        await Task.Delay(100); // Simulating API call
        
        return new PaymentResult
        {
            IsSuccess = true,
            TransactionId = transactionId,
            Message = "Credit card refund processed successfully.",
            Amount = amount
        };
    }
}
