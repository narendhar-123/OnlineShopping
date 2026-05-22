using Shopping.Web.Service.Payments.Interfaces;

namespace Shopping.Web.Service.Payments.Factory;

/// <summary>
/// Enum representing supported payment types.
/// </summary>
public enum PaymentType
{
    CreditCard,
    UPI,
    NetBanking
}

/// <summary>
/// Factory interface for creating payment processors.
/// </summary>
public interface IPaymentFactory
{
    IPaymentProcessor CreatePaymentProcessor(PaymentType paymentType);
}

/// <summary>
/// Factory implementation for creating payment processors.
/// Supports easy addition of new payment methods.
/// </summary>
public class PaymentFactory : IPaymentFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<PaymentType, Type> _paymentProcessors;

    public PaymentFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        
        _paymentProcessors = new Dictionary<PaymentType, Type>
        {
            { PaymentType.CreditCard, typeof(CreditCardPaymentProcessor) },
            { PaymentType.UPI, typeof(UPIPaymentProcessor) },
            { PaymentType.NetBanking, typeof(NetBankingPaymentProcessor) }
        };
    }

    public IPaymentProcessor CreatePaymentProcessor(PaymentType paymentType)
    {
        if (!_paymentProcessors.TryGetValue(paymentType, out var processorType))
        {
            throw new ArgumentException($"Unsupported payment type: {paymentType}", nameof(paymentType));
        }

        var processor = _serviceProvider.GetService(processorType) as IPaymentProcessor;
        
        if (processor == null)
        {
            throw new InvalidOperationException($"Payment processor for {paymentType} is not registered.");
        }

        return processor;
    }
}
