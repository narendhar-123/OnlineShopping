using Shopping.Web.Entities;

namespace Shopping.Web.Repository.Interfaces;

/// <summary>
/// Repository interface for Order-specific data access operations.
/// </summary>
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserEmailAsync(string userEmail);
}
