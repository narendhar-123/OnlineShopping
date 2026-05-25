using Shopping.Web.DTO;
using Shopping.Web.Entities;
using Shopping.Web.Repository.Interfaces;
using Shopping.Web.Service.Interfaces;

namespace Shopping.Web.Service.Implementation;

/// <summary>
/// Service implementation containing business logic for Item operations.
/// </summary>
public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
        var items = await _itemRepository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<ItemDto?> GetItemByIdAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item == null || !item.IsActive)
            return null;

        return MapToDto(item);
    }

    public async Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto)
    {
        var item = new Item
        {
            Name = createItemDto.Name,
            Description = createItemDto.Description,
            Price = createItemDto.Price,
            StockQuantity = createItemDto.StockQuantity,
            Category = createItemDto.Category,
            ImageUrl = createItemDto.ImageUrl,
            IsActive = true
        };

        var created = await _itemRepository.AddAsync(item);
        return MapToDto(created);
    }

    public async Task<ItemDto?> UpdateItemAsync(int id, UpdateItemDto updateItemDto)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item == null || !item.IsActive)
            return null;

        item.Name = updateItemDto.Name;
        item.Description = updateItemDto.Description;
        item.Price = updateItemDto.Price;
        item.StockQuantity = updateItemDto.StockQuantity;
        item.Category = updateItemDto.Category;
        item.ImageUrl = updateItemDto.ImageUrl;

        await _itemRepository.UpdateAsync(item);
        return MapToDto(item);
    }

    public async Task<bool> DeleteItemByIdAsync(int id)
    {
        var exists = await _itemRepository.ExistsAsync(id);
        if (!exists)
            return false;

        await _itemRepository.DeleteAsync(id);
        return true;
    }

    private static ItemDto MapToDto(Item item) => new()
    {
        Id = item.Id,
        Name = item.Name,
        Description = item.Description,
        Price = item.Price,
        StockQuantity = item.StockQuantity,
        Category = item.Category,
        ImageUrl = item.ImageUrl,
        CreatedDate = item.CreatedDate,
        CreatedBy = item.CreatedBy,
        ModifiedDate = item.ModifiedDate,
        ModifiedBy = item.ModifiedBy
    };
}
