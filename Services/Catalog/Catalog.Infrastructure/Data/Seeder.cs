using System.Reflection;
using System.Text.Json;
using MongoDB.Driver;

namespace Catalog.Infra.Data.SeedData;

public static class Seeder
{
    private static readonly string BasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
    public static void SeedData<T>(IMongoCollection<T> collection, string[] filePath) where T : class
    {
        bool isCollectionFilled = collection.Find(b => true).Any();
        
        if (!isCollectionFilled)
        {
            var absolutePath = Path.GetFullPath(Path.Combine(BasePath, filePath[0], filePath[1], filePath[2]));
            var jsonData = File.ReadAllText(absolutePath);
            var entities = JsonSerializer.Deserialize<IEnumerable<T>>(jsonData);
            if (entities.Any())
            {
                collection.InsertMany(entities);
            }
        }
        
        /*
        var absolutePath = Path.GetFullPath(Path.Combine(BasePath, filePath[0], filePath[1], filePath[2]));
        var jsonData = File.ReadAllText(absolutePath);
        var entities = JsonSerializer.Deserialize<IEnumerable<T>>(jsonData);
        collection.DeleteMany(b => true);
        bool isPresent = collection.Find(b => true).Any();
        if (entities.Any())
        {
            collection.InsertMany(entities);
        }
        */
    }
}