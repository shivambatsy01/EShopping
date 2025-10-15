using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Extensions;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (orderContext.Orders.Any())
        {
            return;
        }
        
        orderContext.Orders.AddRange(GetOrders());
        logger.LogInformation("Seeding OrderContext.....");
        await orderContext.SaveChangesAsync();
        logger.LogInformation("Ordering Database Seeding Complete.....");
    }

    private static IEnumerable<Order> GetOrders()
    {
        return new List<Order>()
        {
            new Order()
            {
                Id = Guid.NewGuid(),
                TotalPrice = 123.45M,
                Username = "admin1",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@admin",
                LastModifiedDate = new DateTime(1970, 01, 01),
                LastModifiedBy = "admin",
            },
            new Order()
            {
                Id = Guid.NewGuid(),
                TotalPrice = 150.45M,
                Username = "admin2",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@admin",
                LastModifiedDate = new DateTime(1970, 01, 02),
                LastModifiedBy = "admin",
            },
            new Order()
            {
                Id = Guid.NewGuid(),
                TotalPrice = 250.45M,
                Username = "admin3",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@admin",
                LastModifiedDate = new DateTime(1970, 01, 03),
                LastModifiedBy = "admin",
            }
        };
    }
}