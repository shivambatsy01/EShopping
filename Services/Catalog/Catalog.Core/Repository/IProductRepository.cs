using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Core.Repository;

public interface IProductRepository
{
    Task<Product> GetProductById(string id); //id is in base entity
    
    Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams);
    
    Task<IEnumerable<Product>> GetProductsByName(string name);
    
    Task<IEnumerable<Product>> GetProductsByType(string type);
    
    Task<IEnumerable<Product>> GetProductsByBrand(string brandName);
    
    Task<Product> AddProduct(Product product);
    
    Task<bool> UpdateProduct(Product product);
    
    Task<bool> DeleteProduct(string id);
}