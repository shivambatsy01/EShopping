using Catalog.Core.Entities;
using Catalog.Core.Specs;
using Catalog.Infra.Data.SeedData.Context;
using MongoDB.Bson;
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
    
    public async Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;

        // Filtering
        if (!string.IsNullOrWhiteSpace(catalogSpecParams.Search))
        {
            filter &= builder.Regex(p => p.Name, new BsonRegularExpression(catalogSpecParams.Search, "i"));
        }
        if (!string.IsNullOrWhiteSpace(catalogSpecParams.BrandId))
        {
            filter &= builder.Eq(p => p.ProductBrand.Id, catalogSpecParams.BrandId);
        }
        if (!string.IsNullOrWhiteSpace(catalogSpecParams.TypeId))
        {
            filter &= builder.Eq(p => p.ProductType.Id, catalogSpecParams.TypeId);
        }

        // Sorting
        var sortDefinition = GetSortDefinition(catalogSpecParams.Sort);

        // Query Call with Pagination
        var items = await _context.Products
            .Find(filter)
            .Sort(sortDefinition)
            .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1)) //Mathematics of pagination
            .Limit(catalogSpecParams.PageSize)
            .ToListAsync();
        
        var totalItems = await _context.Products.CountDocumentsAsync(p => true); //all products count

        // Return paginated result
        return new Pagination<Product>
        {
            PageIndex = catalogSpecParams.PageIndex,
            PageSize = catalogSpecParams.PageSize,
            Count = (int)totalItems,
            Items = items,
            MatchingItemsCount = items.Count
        };
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
    
    private SortDefinition<Product> GetSortDefinition(string? sort)
    {
        if (string.IsNullOrWhiteSpace(sort))
            return Builders<Product>.Sort.Ascending(p => p.Name); // default sort

        return sort.ToLower() switch
        {
            "name" => Builders<Product>.Sort.Ascending(p => p.Name),
            "priceasc" => Builders<Product>.Sort.Ascending(p => p.Price),
            "pricedesc" => Builders<Product>.Sort.Descending(p => p.Price),
            _ => Builders<Product>.Sort.Ascending(p => p.Name) // fallback
        };
    }
}