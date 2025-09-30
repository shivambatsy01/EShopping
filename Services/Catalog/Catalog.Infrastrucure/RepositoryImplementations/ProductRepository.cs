using Catalog.Core.Entities;
using Catalog.Infra.Data.SeedData.Context;
using MongoDB.Driver;

namespace Catalog.Core.Repository.RepositoryImplementations;

public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
{
    //Interface declaration is in Core (Business model), and Implementation is in Infrastructure project
    //Clean Architecture
    //We can implement these interfaces separately, but for now, we are implementing in same class

    private ICatalogContext _context; //needs dependency injection

    public ProductRepository(ICatalogContext context)
    {
        this._context = context;
        
    }
    
    public async Task<Product> GetProductById(string id) //note that in interface we didn't define async
    {
        return await _context
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _context
            .Products
            .Find(p => true)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        return await _context
            .Products
            .Find(p => name.ToLower().Equals(p.Name.ToLower()))
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> GetProductsByType(string type)
    {
        return await _context
            .Products
            .Find(p => type.ToLower().Equals(p.ProductType.Name.ToLower()))
            .ToListAsync();

    }
    
    public async Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
    {
        return await _context
            .Products
            .Find(p => brandName.ToLower().Equals(p.ProductBrand.Name.ToLower()))
            .ToListAsync();
    }
    
    public async Task<Product> AddProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }
    
    public async Task<bool> UpdateProduct(Product product)
    {
        //We are not handling any validation here
        var updateResult = await _context
            .Products
            .ReplaceOneAsync(p => p.Id == product.Id, product);
        
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
    
    public async Task<bool> DeleteProduct(string id)
    {
        var deletedProductTask = await _context
            .Products
             .DeleteOneAsync(p => p.Id == id);
        //DeleteOneAsync returns a task
        return deletedProductTask.IsAcknowledged && deletedProductTask.DeletedCount > 0;
        //what is this Acknowledged and DeletedCount
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await _context.ProductBrands.Find(p => true).ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetAllProductTypes()
    {
        return await _context.ProductTypes.Find(p => true).ToListAsync();
    }
}