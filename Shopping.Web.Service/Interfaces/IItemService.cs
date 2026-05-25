using Shopping.Web.DTO;

namespace Shopping.Web.Service.Interfaces;

/// <summary>
/// Service interface for Item business operations.
/// </summary>
public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetItemsAsync();
    Task<ItemDto?> GetItemByIdAsync(int id);
    Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto);
    Task<ItemDto?> UpdateItemAsync(int id, UpdateItemDto updateItemDto);
    Task<bool> DeleteItemByIdAsync(int id);
}
