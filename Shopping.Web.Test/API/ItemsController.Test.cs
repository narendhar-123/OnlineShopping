using Microsoft.AspNetCore.Mvc;
using Moq;
using Shopping.Web.Api.Controllers;
using Shopping.Web.DTO;
using Shopping.Web.Service.Interfaces;

namespace Shopping.Web.Test.API;

public class ItemsControllerTest
{
    private readonly Mock<IItemService> _mockItemService;
    private readonly ItemsController _controller;

    public ItemsControllerTest()
    {
        _mockItemService = new Mock<IItemService>();
        _controller = new ItemsController(_mockItemService.Object);
    }

    [Fact]
    public async Task GetItems_ReturnsOkWithItems()
    {
        var items = new List<ItemDto> { new() { Id = 1, Name = "Item1", Price = 10.00m } };
        _mockItemService.Setup(s => s.GetItemsAsync()).ReturnsAsync(items);

        var result = await _controller.GetItems();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(items, ok.Value);
    }

    [Fact]
    public async Task GetItemById_ExistingId_ReturnsOkWithItem()
    {
        var item = new ItemDto { Id = 1, Name = "Item1", Price = 10.00m };
        _mockItemService.Setup(s => s.GetItemByIdAsync(1)).ReturnsAsync(item);

        var result = await _controller.GetItemById(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(item, ok.Value);
    }

    [Fact]
    public async Task GetItemById_NonExistingId_ReturnsNotFound()
    {
        _mockItemService.Setup(s => s.GetItemByIdAsync(99)).ReturnsAsync((ItemDto?)null);

        var result = await _controller.GetItemById(99);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateItem_ValidDto_ReturnsCreated()
    {
        var createDto = new CreateItemDto { Name = "Item1", Price = 10.00m, Category = "Cat1" };
        var created = new ItemDto { Id = 1, Name = "Item1", Price = 10.00m };
        _mockItemService.Setup(s => s.CreateItemAsync(createDto)).ReturnsAsync(created);

        var result = await _controller.CreateItem(createDto);

        var createdAt = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(created, createdAt.Value);
    }

    [Fact]
    public async Task UpdateItem_ExistingId_ReturnsOkWithUpdatedItem()
    {
        var updateDto = new UpdateItemDto { Name = "Updated", Price = 20.00m, Category = "Cat2" };
        var updated = new ItemDto { Id = 1, Name = "Updated", Price = 20.00m };
        _mockItemService.Setup(s => s.UpdateItemAsync(1, updateDto)).ReturnsAsync(updated);

        var result = await _controller.UpdateItem(1, updateDto);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(updated, ok.Value);
    }

    [Fact]
    public async Task UpdateItem_NonExistingId_ReturnsNotFound()
    {
        var updateDto = new UpdateItemDto { Name = "Updated", Price = 20.00m, Category = "Cat2" };
        _mockItemService.Setup(s => s.UpdateItemAsync(99, updateDto)).ReturnsAsync((ItemDto?)null);

        var result = await _controller.UpdateItem(99, updateDto);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteItemById_ExistingId_ReturnsNoContent()
    {
        _mockItemService.Setup(s => s.DeleteItemByIdAsync(1)).ReturnsAsync(true);

        var result = await _controller.DeleteItemById(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteItemById_NonExistingId_ReturnsNotFound()
    {
        _mockItemService.Setup(s => s.DeleteItemByIdAsync(99)).ReturnsAsync(false);

        var result = await _controller.DeleteItemById(99);

        Assert.IsType<NotFoundResult>(result);
    }
}
