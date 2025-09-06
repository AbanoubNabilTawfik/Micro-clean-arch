using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductType> typeCollection)
        {
            var hasTypes = await typeCollection.Find(_ => true).AnyAsync();
            if (hasTypes)
                return;

            var filePath = Path.Combine("Data", "SeedData", "types.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed file not exists:{filePath}");
                return;
            }
            var typeData = await File.ReadAllTextAsync(filePath);
            var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

            if (types?.Any() is true)
            {
                await typeCollection.InsertManyAsync(types);
            }
        }
    }
}
