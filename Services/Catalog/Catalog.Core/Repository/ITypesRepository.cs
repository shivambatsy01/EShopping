using Catalog.Core.Entities;

namespace Catalog.Core.Repository;

public interface ITypesRepository
{
    Task<IEnumerable<ProductType>> GetAllProductTypes();
}