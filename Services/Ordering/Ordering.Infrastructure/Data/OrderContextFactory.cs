using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ordering.Infrastructure.Extensions;

public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    //Code first approach
    public OrderContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        optionsBuilder.UseSqlServer("Data Source=OrderDb");
        return new OrderContext(optionsBuilder.Options);
    }
}