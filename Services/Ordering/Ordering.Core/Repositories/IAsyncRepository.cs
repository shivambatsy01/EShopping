using System.Linq.Expressions;
using Ordering.Core.Commons;

namespace Ordering.Core.Repositories;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T Entity);
    Task UpdateAsync(T Entity);
    Task DeleteAsync(T Entity);
}