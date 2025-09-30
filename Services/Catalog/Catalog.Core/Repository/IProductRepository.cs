using Catalog.Core.Entities;

namespace Catalog.Core.Repository;

public interface IProductRepository
{
    Task<Product> GetProductById(string id); //id is in base entity
    
    Task<IEnumerable<Product>> GetAllProducts();
    
    Task<IEnumerable<Product>> GetProductsByName(string name);
    
    Task<IEnumerable<Product>> GetProductsByType(string type);
    
    Task<IEnumerable<Product>> GetProductsByBrand(string brandName);
    
    Task<Product> AddProduct(Product product);
    
    Task<bool> UpdateProduct(Product product);
    
    Task<bool> DeleteProduct(string id);
}