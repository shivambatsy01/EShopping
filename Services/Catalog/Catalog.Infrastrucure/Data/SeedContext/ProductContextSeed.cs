using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infra.Data.SeedData;

public static class ProductContextSeed
{
    private static readonly string Path = "Data/SeedData/product.json";
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        //is MongoCollection is static dependency ?
        //this code part can be reused as we are writing same code again and again.
        //Use a Generic type : and seed the data. Serializer and deserializer using generic or Object
        
        Seeder.SeedData(productCollection, Path);
    }
}