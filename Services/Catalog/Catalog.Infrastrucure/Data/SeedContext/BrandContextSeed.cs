using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infra.Data;

public static class BrandContextSeed //static class to seed the data in context class
{
    private static readonly string Path = "Data/SeedData/brands.json";
    public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
    {
        //if brandCollection is empty, 
        
        //We can also directly call : Seeder.SeedData(productCollection, Path); as this method is written generic

        bool isBrandPresent = brandCollection.Find(b => true).Any();
        
        if(isBrandPresent) return; //return-if to reduce if-nesting
        
        var brandsData = File.ReadAllText(Path);
        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
        
        if(brands == null) return; //return-if to reduce nesting
        
        foreach (var brand in brands)
        {
            brandCollection.InsertOne(brand);
        }
    }
}