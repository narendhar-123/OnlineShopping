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

    public DbSet<Item> Items => Set<Item>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Entity configurations will be applied here
    }
}
