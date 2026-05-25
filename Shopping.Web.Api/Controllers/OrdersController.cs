using Microsoft.AspNetCore.Mvc;
using Shopping.Web.DTO;
using Shopping.Web.Service.Interfaces;

namespace Shopping.Web.Api.Controllers;

/// <summary>
/// API controller for managing orders in the shopping application.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    /// <summary>
    /// Places a new order.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _orderService.CreateOrderAsync(createOrderDto);
        return CreatedAtAction(nameof(CreateOrder), new { id = result.OrderId }, result);
    }
}
