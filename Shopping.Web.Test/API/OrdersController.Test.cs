using Microsoft.AspNetCore.Mvc;
using Moq;
using Shopping.Web.Api.Controllers;
using Shopping.Web.DTO;
using Shopping.Web.Service.Interfaces;

namespace Shopping.Web.Test.API;

public class OrdersControllerTest
{
    private readonly Mock<IOrderService> _mockOrderService;
    private readonly OrdersController _controller;

    public OrdersControllerTest()
    {
        _mockOrderService = new Mock<IOrderService>();
        _controller = new OrdersController(_mockOrderService.Object);
    }

    [Fact]
    public async Task CreateOrder_ValidDto_ReturnsCreated()
    {
        var createDto = new CreateOrderDto
        {
            ItemId = 1,
            ItemType = "Electronics",
            UserEmail = "user@example.com",
            DeliveryLocation = "Chennai, Tamil Nadu",
            PostalCode = "600001",
            DeliveryDate = DateTime.UtcNow.AddDays(3),
            PaymentType = "CreditCard"
        };
        var response = new OrderResponseDto { OrderId = 42, Message = "Order created successfully." };
        _mockOrderService.Setup(s => s.CreateOrderAsync(createDto)).ReturnsAsync(response);

        var result = await _controller.CreateOrder(createDto);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(response, created.Value);
    }

    [Fact]
    public async Task CreateOrder_InvalidModelState_ReturnsBadRequest()
    {
        _controller.ModelState.AddModelError("UserEmail", "UserEmail must be a valid email address.");

        var result = await _controller.CreateOrder(new CreateOrderDto());

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateOrder_ServiceThrowsArgumentException_PropagatesException()
    {
        var createDto = new CreateOrderDto
        {
            ItemId = 1,
            ItemType = "Digital",
            UserEmail = "user@example.com",
            DeliveryLocation = "Mumbai",
            PostalCode = "400001",
            DeliveryDate = DateTime.UtcNow.AddDays(2),
            PaymentType = "CashOnDelivery"
        };
        _mockOrderService
            .Setup(s => s.CreateOrderAsync(createDto))
            .ThrowsAsync(new InvalidOperationException("ItemType 'Digital' does not support Cash on Delivery."));

        await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.CreateOrder(createDto));
    }
}
