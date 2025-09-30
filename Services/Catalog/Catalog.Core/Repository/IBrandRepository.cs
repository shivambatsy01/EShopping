using Catalog.Core.Entities;

namespace Catalog.Core.Repository;

public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllBrands();
}