using Microsoft.EntityFrameworkCore;
using Shopping.Web.Entities;
using Shopping.Web.Repository.Data;
using Shopping.Web.Repository.Interfaces;

namespace Shopping.Web.Repository.Implementation;

/// <summary>
/// Repository implementation for Item data access operations.
/// </summary>
public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    public ItemRepository(ShoppingDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Item>> GetByCategoryAsync(string category)
    {
        return await _dbSet
            .Where(i => i.IsActive && i.Category == category)
            .ToListAsync();
    }
}
