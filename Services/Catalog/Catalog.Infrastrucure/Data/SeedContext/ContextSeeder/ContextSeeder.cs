using System.Text.Json;
using MongoDB.Driver;

namespace Catalog.Infra.Data.SeedContext.ContextSeeder;

public static class ContextSeeder
{
    public static void SeedData<T>(IMongoCollection<T> collection, string jsonFilePath) where T : class
    {
        //if brandCollection is empty, 
        
        //We can also directly call : Seeder.SeedData(productCollection, Path); as this method is written generic

        bool isDataPresent = collection.Find(_ => true).Any();
        
        if(isDataPresent) return; //return-if to reduce if-nesting
        
        var jsonData = File.ReadAllText(jsonFilePath);
        var entitiesList = JsonSerializer.Deserialize<List<T>>(jsonData);
        
        if(entitiesList == null) return; //return-if to reduce nesting
        
        foreach (var item in entitiesList)
        {
            collection.InsertOne(item);
        }
    }
}