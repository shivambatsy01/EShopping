using System.Text.Json;
using Catalog.Core.Entities;
using Catalog.Infra.Data.SeedData;
using MongoDB.Driver;

namespace Catalog.Infra.Data;

public static class ProductTypeContextSeed
{
    private static readonly string Path = "Data/SeedData/types.json";
    public static void SeedData(IMongoCollection<ProductType> typeCollection)
    {
        Seeder.SeedData(typeCollection, Path);
    }
}