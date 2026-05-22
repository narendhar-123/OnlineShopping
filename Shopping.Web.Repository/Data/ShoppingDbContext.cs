using Microsoft.EntityFrameworkCore;
using Shopping.Web.Entities;

namespace Shopping.Web.Repository.Data;

/// <summary>
/// Database context for the Shopping application.
/// </summary>
public class ShoppingDbContext : DbContext
{
    public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
        : base(options)
    {
    }

    // DbSets will be added here as entities are created
    // Example: public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Entity configurations will be applied here
    }
}
