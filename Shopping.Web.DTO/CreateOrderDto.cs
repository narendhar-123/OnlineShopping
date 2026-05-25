using System.ComponentModel.DataAnnotations;

namespace Shopping.Web.DTO;

/// <summary>
/// DTO for creating a new order.
/// </summary>
public class CreateOrderDto
{
    [Required(ErrorMessage = "ItemId is required.")]
    public int ItemId { get; set; }

    [Required(ErrorMessage = "ItemType is required.")]
    public string ItemType { get; set; } = string.Empty;

    [Required(ErrorMessage = "UserEmail is required.")]
    [EmailAddress(ErrorMessage = "UserEmail must be a valid email address.")]
    public string UserEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "DeliveryLocation is required.")]
    public string DeliveryLocation { get; set; } = string.Empty;

    [Required(ErrorMessage = "PostalCode is required.")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "PostalCode must be a valid 6-digit Indian PIN code.")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "DeliveryDate is required.")]
    public DateTime DeliveryDate { get; set; }

    public int? DeliveryPartnerId { get; set; }

    public string? DeliveryPartnerName { get; set; }

    public bool IsDelicateItem { get; set; }

    public string PackingType { get; set; } = "Standard";

    public bool IsExpressDelivery { get; set; }

    [Required(ErrorMessage = "PaymentType is required.")]
    public string PaymentType { get; set; } = string.Empty;
}
