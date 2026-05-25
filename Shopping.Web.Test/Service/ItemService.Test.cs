using Moq;
using Shopping.Web.DTO;
using Shopping.Web.Entities;
using Shopping.Web.Repository.Interfaces;
using Shopping.Web.Service.Implementation;

namespace Shopping.Web.Test.Service;

public class ItemServiceTest
{
    private readonly Mock<IItemRepository> _mockRepo;
    private readonly ItemService _service;

    public ItemServiceTest()
    {
        _mockRepo = new Mock<IItemRepository>();
        _service = new ItemService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetItemsAsync_ReturnsAllActiveItems()
    {
        var items = new List<Item>
        {
            new() { Id = 1, Name = "Item1", Price = 10m, IsActive = true, CreatedDate = DateTime.UtcNow }
        };
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

        var result = await _service.GetItemsAsync();

        Assert.Single(result);
        Assert.Equal("Item1", result.First().Name);
    }

    [Fact]
    public async Task GetItemByIdAsync_ExistingActiveItem_ReturnsItemDto()
    {
        var item = new Item { Id = 1, Name = "Item1", Price = 10m, IsActive = true, CreatedDate = DateTime.UtcNow };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(item);

        var result = await _service.GetItemByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetItemByIdAsync_NotFound_ReturnsNull()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Item?)null);

        var result = await _service.GetItemByIdAsync(99);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateItemAsync_ValidDto_ReturnsCreatedItemDto()
    {
        var createDto = new CreateItemDto { Name = "NewItem", Price = 15m, Category = "Cat" };
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Item>()))
            .ReturnsAsync((Item i) => { i.Id = 1; i.CreatedDate = DateTime.UtcNow; return i; });

        var result = await _service.CreateItemAsync(createDto);

        Assert.Equal("NewItem", result.Name);
        Assert.Equal(15m, result.Price);
    }

    [Fact]
    public async Task UpdateItemAsync_ExistingItem_ReturnsUpdatedDto()
    {
        var item = new Item { Id = 1, Name = "Old", Price = 5m, IsActive = true, CreatedDate = DateTime.UtcNow };
        var updateDto = new UpdateItemDto { Name = "Updated", Price = 20m, Category = "NewCat" };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(item);
        _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Item>())).Returns(Task.CompletedTask);

        var result = await _service.UpdateItemAsync(1, updateDto);

        Assert.NotNull(result);
        Assert.Equal("Updated", result!.Name);
        Assert.Equal(20m, result.Price);
    }

    [Fact]
    public async Task UpdateItemAsync_NotFound_ReturnsNull()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Item?)null);

        var result = await _service.UpdateItemAsync(99, new UpdateItemDto { Name = "X", Price = 1m, Category = "Y" });

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteItemByIdAsync_ExistingItem_ReturnsTrue()
    {
        _mockRepo.Setup(r => r.ExistsAsync(1)).ReturnsAsync(true);
        _mockRepo.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        var result = await _service.DeleteItemByIdAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteItemByIdAsync_NotFound_ReturnsFalse()
    {
        _mockRepo.Setup(r => r.ExistsAsync(99)).ReturnsAsync(false);

        var result = await _service.DeleteItemByIdAsync(99);

        Assert.False(result);
    }
}
