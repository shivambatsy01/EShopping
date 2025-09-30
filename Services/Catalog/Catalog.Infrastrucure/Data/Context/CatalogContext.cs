using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Reflection;

namespace Catalog.Infra.Data.SeedData.Context;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<ProductType> ProductTypes { get; }
    public IMongoCollection<ProductBrand> ProductBrands {get; }

    public CatalogContext(IConfiguration config)
    {
        /* configurations are in appsettings.json of CatalogAPI project
         Because main startup project is API project, Rest all are class library projects only
         */
        var connectionString = config["DatabaseSettings:MongoCatalog:ConnectionString"];
        var databaseName = config["DatabaseSettings:MongoCatalog:DatabaseName"];
        var productCollectionName = config["DatabaseSettings:MongoCatalog:ProductCollectionName"];
        var productBrandCollectionName = config["DatabaseSettings:MongoCatalog:ProductBrandCollectionName"];
        var productTypesCollectionName = config["DatabaseSettings:MongoCatalog:ProductTypesCollectionName"];
        
        //Alternate way to read from config :
        var connectionString2 = config.GetValue<string>("DatabaseSettings:MongoCatalog:ConnectionString");
        // these connection strings are picked from docker running instance of MongoDB
            
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        Products = database.GetCollection<Product>(productCollectionName);
        ProductTypes = database.GetCollection<ProductType>(productTypesCollectionName);
        ProductBrands = database.GetCollection<ProductBrand>(productBrandCollectionName);
        
        BrandContextSeed.SeedData(ProductBrands);
        ProductContextSeed.SeedData(Products);
        ProductTypeContextSeed.SeedData(ProductTypes);
    }
}