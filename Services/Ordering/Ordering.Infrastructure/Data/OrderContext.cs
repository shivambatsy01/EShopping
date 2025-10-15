using Microsoft.EntityFrameworkCore;
using Ordering.Core.Commons;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Infrastructure.Extensions;

//Signature style of EntityFramework DB context writing
public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
        
    }
    
    public DbSet<Order> Orders { get; set; } //EntityFramework DBSet

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) //Use default cancellation token
    {
        foreach (var entry in base.ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "test"; //To Do, will be replaced (When Implement Identity Server)
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "test"; //To Do, needs to be updated (when implement Identity Server)
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}