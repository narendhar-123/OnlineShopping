using Shopping.Web.DTO;

namespace Shopping.Web.Service.Interfaces;

/// <summary>
/// Service interface for Order business logic operations.
/// </summary>
public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto);
}
