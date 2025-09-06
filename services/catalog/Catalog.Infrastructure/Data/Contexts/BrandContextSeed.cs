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
    public static class BrandContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductBrand> brandCollection)
        {
            var hasBrands = await brandCollection.Find(_ => true).AnyAsync();
            if (hasBrands)
                return;

            var filePath = Path.Combine("Data", "SeedData", "brands.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed file not exists:{filePath}");
                return;
            }
            var brandData = await File.ReadAllTextAsync(filePath);
            var brands=JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

            if (brands?.Any() is true)
            {
              await brandCollection.InsertManyAsync(brands);
            }
        }
    }
}
