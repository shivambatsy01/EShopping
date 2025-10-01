using System.Text.Json;
using Catalog.Core.Entities;
using Catalog.Infra.Data.SeedData;
using MongoDB.Driver;

namespace Catalog.Infra.Data;

public static class BrandContextSeed //static class to seed the data in context class
{
    private static readonly string[] BrandFilePathParts = new string[]
    {
        "Data",
        "SeedData",
        "brands.json"
    };
    public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
    { 
        Seeder.SeedData(brandCollection, BrandFilePathParts);
    }
}