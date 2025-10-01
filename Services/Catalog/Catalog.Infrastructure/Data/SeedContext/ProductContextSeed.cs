using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infra.Data.SeedData;

public static class ProductContextSeed
{
    private static readonly string[] ProductFilePathParts = new string[]
    {
        "Data",
        "SeedData",
        "products.json"
    };
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        Seeder.SeedData(productCollection, ProductFilePathParts);
    }
}