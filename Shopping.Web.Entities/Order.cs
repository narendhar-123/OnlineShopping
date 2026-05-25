using Shopping.Web.Entities.Enums;

namespace Shopping.Web.Entities;

/// <summary>
/// Represents a customer order in the shopping application.
/// </summary>
public class Order : BaseEntity
{
    public int ItemId { get; set; }
    public ItemType ItemType { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string DeliveryLocation { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public DateTime DeliveryDate { get; set; }
    public int? DeliveryPartnerId { get; set; }
    public string? DeliveryPartnerName { get; set; }
    public bool IsDelicateItem { get; set; }
    public PackingType PackingType { get; set; }
    public bool IsExpressDelivery { get; set; }
    public PaymentMethod PaymentType { get; set; }
    public string? PaymentTransactionId { get; set; }
}
