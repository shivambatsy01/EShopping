using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infra.Data.SeedData.Context;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
    IMongoCollection<ProductType> ProductTypes { get; }
    IMongoCollection<ProductBrand> ProductBrands {get;}
}