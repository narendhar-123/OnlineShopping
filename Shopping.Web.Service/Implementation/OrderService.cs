using Shopping.Web.DTO;
using Shopping.Web.Entities;
using Shopping.Web.Entities.Enums;
using Shopping.Web.Repository.Interfaces;
using Shopping.Web.Service.Interfaces;
using Shopping.Web.Service.Payments.Factory;
using Shopping.Web.Service.Payments.Interfaces;

namespace Shopping.Web.Service.Implementation;

/// <summary>
/// Service implementation containing business logic for Order operations.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentFactory _paymentFactory;

    // Item types that do NOT support Cash on Delivery
    private static readonly HashSet<ItemType> CodUnsupportedItemTypes =
        new() { ItemType.Digital };

    public OrderService(IOrderRepository orderRepository, IPaymentFactory paymentFactory)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _paymentFactory = paymentFactory ?? throw new ArgumentNullException(nameof(paymentFactory));
    }

    public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        // Validation 1: ItemId and ItemType are required (handled by model annotations,
        // but also guard here)
        if (createOrderDto.ItemId <= 0)
            throw new ArgumentException("ItemId is required and must be a positive integer.");

        if (!Enum.TryParse<ItemType>(createOrderDto.ItemType, ignoreCase: true, out var itemType))
            throw new ArgumentException($"ItemType '{createOrderDto.ItemType}' is not valid.");

        // Validation 2: UserEmail must be valid (model annotation covers format;
        // guard against empty)
        if (string.IsNullOrWhiteSpace(createOrderDto.UserEmail))
            throw new ArgumentException("UserEmail is required.");

        // Validation 3 & 4: Payment type validation
        if (!Enum.TryParse<PaymentType>(createOrderDto.PaymentType, ignoreCase: true, out var paymentType))
            throw new ArgumentException($"PaymentType '{createOrderDto.PaymentType}' is not supported.");

        if (paymentType == PaymentType.CashOnDelivery && CodUnsupportedItemTypes.Contains(itemType))
            throw new InvalidOperationException($"ItemType '{itemType}' does not support Cash on Delivery.");

        string? transactionId = null;
        if (paymentType != PaymentType.CashOnDelivery)
        {
            var processor = _paymentFactory.CreatePaymentProcessor(paymentType);
            var paymentResult = await processor.ProcessPaymentAsync(new PaymentRequest
            {
                Amount = 0, // Amount resolved from item; placeholder for order creation flow
                OrderId = Guid.NewGuid().ToString(),
                CustomerId = createOrderDto.UserEmail
            });

            if (!paymentResult.IsSuccess)
                throw new InvalidOperationException($"Payment failed: {paymentResult.Message}");

            transactionId = paymentResult.TransactionId;
        }

        // Validation 5: Indian postcode (6-digit) — enforced by model annotation;
        // additional guard
        if (!System.Text.RegularExpressions.Regex.IsMatch(createOrderDto.PostalCode, @"^\d{6}$"))
            throw new ArgumentException("PostalCode must be a valid 6-digit Indian PIN code.");

        if (!Enum.TryParse<PackingType>(createOrderDto.PackingType, ignoreCase: true, out var packingType))
            packingType = PackingType.Standard;

        var order = new Order
        {
            ItemId = createOrderDto.ItemId,
            ItemType = itemType,
            UserEmail = createOrderDto.UserEmail,
            DeliveryLocation = createOrderDto.DeliveryLocation,
            PostalCode = createOrderDto.PostalCode,
            DeliveryDate = createOrderDto.DeliveryDate,
            DeliveryPartnerId = createOrderDto.DeliveryPartnerId,
            DeliveryPartnerName = createOrderDto.DeliveryPartnerName,
            IsDelicateItem = createOrderDto.IsDelicateItem,
            PackingType = packingType,
            IsExpressDelivery = createOrderDto.IsExpressDelivery,
            PaymentType = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), createOrderDto.PaymentType, ignoreCase: true),
            PaymentTransactionId = transactionId,
            IsActive = true
        };

        Order created;
        try
        {
            created = await _orderRepository.AddAsync(order);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unable to create order.", ex);
        }

        return new OrderResponseDto
        {
            OrderId = created.Id,
            Message = "Order created successfully."
        };
    }
}
