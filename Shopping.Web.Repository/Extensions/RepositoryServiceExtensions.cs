using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Web.Repository.Data;
using Shopping.Web.Repository.Implementation;
using Shopping.Web.Repository.Interfaces;

namespace Shopping.Web.Repository.Extensions;

/// <summary>
/// Extension methods for registering repository layer services.
/// </summary>
public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositoryServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext with Azure SQL connection
        services.AddDbContext<ShoppingDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()));

        // Register Generic Repository
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Additional repositories will be registered here

        return services;
    }
}
