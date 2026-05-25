namespace Shopping.Web.DTO;

/// <summary>
/// Response DTO returned after successfully creating an order.
/// </summary>
public class OrderResponseDto
{
    public int OrderId { get; set; }
    public string Message { get; set; } = string.Empty;
}
