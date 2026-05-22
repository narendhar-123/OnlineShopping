using Shopping.Web.Service.Payments.Interfaces;

namespace Shopping.Web.Service.Payments.Factory;

/// <summary>
/// UPI payment processor implementation.
/// </summary>
public class UPIPaymentProcessor : IPaymentProcessor
{
    public string PaymentType => "UPI";

    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        // TODO: Implement actual UPI payment processing logic
        // This would integrate with UPI payment gateway APIs
        
        await Task.Delay(100); // Simulating API call
        
        return new PaymentResult
        {
            IsSuccess = true,
            TransactionId = Guid.NewGuid().ToString(),
            Message = "UPI payment processed successfully.",
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
            Message = "UPI refund processed successfully.",
            Amount = amount
        };
    }
}
