using Shopping.Web.Service.Payments.Interfaces;

namespace Shopping.Web.Service.Payments.Factory;

/// <summary>
/// Net Banking payment processor implementation.
/// </summary>
public class NetBankingPaymentProcessor : IPaymentProcessor
{
    public string PaymentType => "NetBanking";

    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        // TODO: Implement actual Net Banking payment processing logic
        // This would integrate with banking gateway APIs
        
        await Task.Delay(100); // Simulating API call
        
        return new PaymentResult
        {
            IsSuccess = true,
            TransactionId = Guid.NewGuid().ToString(),
            Message = "Net Banking payment processed successfully.",
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
            Message = "Net Banking refund processed successfully.",
            Amount = amount
        };
    }
}
