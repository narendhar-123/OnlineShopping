using Shopping.Web.Entities;
using Shopping.Web.Repository.Interfaces;

namespace Shopping.Web.Repository.Interfaces;

/// <summary>
/// Repository interface for Item-specific data access operations.
/// </summary>
public interface IItemRepository : IGenericRepository<Item>
{
    Task<IEnumerable<Item>> GetByCategoryAsync(string category);
}
