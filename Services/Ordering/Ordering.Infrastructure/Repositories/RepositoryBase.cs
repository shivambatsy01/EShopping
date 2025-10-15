using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Core.Commons;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Extensions;

namespace Ordering.Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly OrderContext _orderContext;
    
    public RepositoryBase(OrderContext orderContext)
    {
        this._orderContext = orderContext;
    }
    
    
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _orderContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _orderContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _orderContext.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T Entity)
    {
        await _orderContext.Set<T>().AddAsync(Entity);
        await _orderContext.SaveChangesAsync();
        return Entity;
    }

    public async Task UpdateAsync(T Entity)
    {
        _orderContext.Entry(Entity).State = EntityState.Modified;
        await _orderContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T Entity)
    {
        _orderContext.Set<T>().Remove(Entity);
        await _orderContext.SaveChangesAsync();
    }
}