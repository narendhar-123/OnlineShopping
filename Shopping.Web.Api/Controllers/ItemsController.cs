using Microsoft.AspNetCore.Mvc;
using Shopping.Web.DTO;
using Shopping.Web.Service.Interfaces;

namespace Shopping.Web.Api.Controllers;

/// <summary>
/// API controller for managing items in the shopping application.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
    }

    /// <summary>
    /// Gets all active items.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetItems()
    {
        var items = await _itemService.GetItemsAsync();
        return Ok(items);
    }

    /// <summary>
    /// Gets an item by its ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _itemService.GetItemByIdAsync(id);
        if (item == null)
            return NotFound();

        return Ok(item);
    }

    /// <summary>
    /// Creates a new item.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateItem([FromBody] CreateItemDto createItemDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _itemService.CreateItemAsync(createItemDto);
        return CreatedAtAction(nameof(GetItemById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Updates an existing item.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateItemDto updateItemDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _itemService.UpdateItemAsync(id, updateItemDto);
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    /// <summary>
    /// Deletes an item by its ID (soft delete).
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteItemById(int id)
    {
        var deleted = await _itemService.DeleteItemByIdAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
