using System.Text.Json;
using MongoDB.Driver;

namespace Catalog.Infra.Data.SeedData;

public static class Seeder
{
    public static void SeedData<T>(IMongoCollection<T> collection, string filePath) where T : class
    {
        bool isCollectionFilled = collection.Find(b => true).Any();

        if (!isCollectionFilled)
        {
            var jsonData = File.ReadAllText(filePath);
            var entities = JsonSerializer.Deserialize<IEnumerable<T>>(jsonData);
            if (entities.Any())
            {
                collection.InsertMany(entities);
            }
        }
    }
}