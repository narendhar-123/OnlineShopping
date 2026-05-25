using Microsoft.EntityFrameworkCore;
using Shopping.Web.Entities;
using Shopping.Web.Repository.Data;
using Shopping.Web.Repository.Interfaces;

namespace Shopping.Web.Repository.Implementation;

/// <summary>
/// Repository implementation for Order data access operations.
/// </summary>
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ShoppingDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserEmailAsync(string userEmail)
    {
        return await _dbSet
            .Where(o => o.IsActive && o.UserEmail == userEmail)
            .ToListAsync();
    }
}
