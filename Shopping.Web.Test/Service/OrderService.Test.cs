using Moq;
using Shopping.Web.DTO;
using Shopping.Web.Entities;
using Shopping.Web.Repository.Interfaces;
using Shopping.Web.Service.Implementation;
using Shopping.Web.Service.Payments.Factory;
using Shopping.Web.Service.Payments.Interfaces;

namespace Shopping.Web.Test.Service;

public class OrderServiceTest
{
    private readonly Mock<IOrderRepository> _mockOrderRepo;
    private readonly Mock<IPaymentFactory> _mockPaymentFactory;
    private readonly OrderService _service;

    public OrderServiceTest()
    {
        _mockOrderRepo = new Mock<IOrderRepository>();
        _mockPaymentFactory = new Mock<IPaymentFactory>();
        _service = new OrderService(_mockOrderRepo.Object, _mockPaymentFactory.Object);
    }

    private static CreateOrderDto BuildValidDto(string paymentType = "CreditCard", string itemType = "Electronics") =>
        new()
        {
            ItemId = 1,
            ItemType = itemType,
            UserEmail = "user@example.com",
            DeliveryLocation = "Chennai, Tamil Nadu",
            PostalCode = "600001",
            DeliveryDate = DateTime.UtcNow.AddDays(3),
            PackingType = "Standard",
            PaymentType = paymentType
        };

    [Fact]
    public async Task CreateOrderAsync_ValidCreditCard_ReturnsOrderId()
    {
        var dto = BuildValidDto("CreditCard");
        var mockProcessor = new Mock<IPaymentProcessor>();
        mockProcessor.Setup(p => p.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
            .ReturnsAsync(new PaymentResult { IsSuccess = true, TransactionId = "TXN123" });

        _mockPaymentFactory.Setup(f => f.CreatePaymentProcessor(PaymentType.CreditCard))
            .Returns(mockProcessor.Object);

        _mockOrderRepo.Setup(r => r.AddAsync(It.IsAny<Order>()))
            .ReturnsAsync((Order o) => { o.Id = 10; return o; });

        var result = await _service.CreateOrderAsync(dto);

        Assert.Equal(10, result.OrderId);
        Assert.Equal("Order created successfully.", result.Message);
    }

    [Fact]
    public async Task CreateOrderAsync_CashOnDelivery_SkipsPaymentProcessor()
    {
        var dto = BuildValidDto("CashOnDelivery", "Electronics");

        _mockOrderRepo.Setup(r => r.AddAsync(It.IsAny<Order>()))
            .ReturnsAsync((Order o) => { o.Id = 20; return o; });

        var result = await _service.CreateOrderAsync(dto);

        Assert.Equal(20, result.OrderId);
        _mockPaymentFactory.Verify(f => f.CreatePaymentProcessor(It.IsAny<PaymentType>()), Times.Never);
    }

    [Fact]
    public async Task CreateOrderAsync_DigitalItemWithCOD_ThrowsInvalidOperationException()
    {
        var dto = BuildValidDto("CashOnDelivery", "Digital");

        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateOrderAsync(dto));
    }

    [Fact]
    public async Task CreateOrderAsync_PaymentFails_ThrowsInvalidOperationException()
    {
        var dto = BuildValidDto("UPI");
        var mockProcessor = new Mock<IPaymentProcessor>();
        mockProcessor.Setup(p => p.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
            .ReturnsAsync(new PaymentResult { IsSuccess = false, Message = "Insufficient funds." });

        _mockPaymentFactory.Setup(f => f.CreatePaymentProcessor(PaymentType.UPI))
            .Returns(mockProcessor.Object);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateOrderAsync(dto));
        Assert.Contains("Payment failed", ex.Message);
    }

    [Fact]
    public async Task CreateOrderAsync_InvalidEmail_ThrowsArgumentException()
    {
        var dto = BuildValidDto();
        dto.UserEmail = string.Empty;

        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateOrderAsync(dto));
    }

    [Fact]
    public async Task CreateOrderAsync_InvalidItemId_ThrowsArgumentException()
    {
        var dto = BuildValidDto();
        dto.ItemId = 0;

        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateOrderAsync(dto));
    }

    [Fact]
    public async Task CreateOrderAsync_InvalidPostalCode_ThrowsArgumentException()
    {
        var dto = BuildValidDto();
        dto.PostalCode = "ABC12"; // invalid

        var mockProcessor = new Mock<IPaymentProcessor>();
        mockProcessor.Setup(p => p.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
            .ReturnsAsync(new PaymentResult { IsSuccess = true, TransactionId = "TXN999" });
        _mockPaymentFactory.Setup(f => f.CreatePaymentProcessor(It.IsAny<PaymentType>()))
            .Returns(mockProcessor.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateOrderAsync(dto));
    }

    [Fact]
    public async Task CreateOrderAsync_InvalidItemType_ThrowsArgumentException()
    {
        var dto = BuildValidDto();
        dto.ItemType = "Unknown";

        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateOrderAsync(dto));
    }
}
