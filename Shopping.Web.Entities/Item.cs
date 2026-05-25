namespace Shopping.Web.Entities;

/// <summary>
/// Represents a product item available for purchase in the shopping application.
/// </summary>
public class Item : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}
