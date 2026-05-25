namespace Shopping.Web.Entities.Enums;

/// <summary>
/// Represents the type/category of an item.
/// Digital items do not support Cash on Delivery.
/// </summary>
public enum ItemType
{
    Electronics,
    Clothing,
    Food,
    Digital,
    Furniture,
    Books
}
