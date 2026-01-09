using Microsoft.EntityFrameworkCore;
using StorageApp.Data;
using StorageApp.Models;
using Xunit;

namespace StorageApp.UnitTest
{
    public class ProductUnitTests
    {
        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task Test_AddProduct()
        {
            using var db = CreateDbContext();
            var repo = new ProductRepository(db);

            String pid = RandomString(5);

            var product = new Product
            {
                Id = pid,
                Name = "SSD 1TB",
                Description = "SSD",
                Price = "100.00",
                Stock = 45
            };

            await repo.AddProductsAsync(product);

            var saved = await db.Products.FirstOrDefaultAsync(p => p.Id == pid);
            Assert.NotNull(saved);
            Assert.Equal("SSD 1TB", saved!.Name);
        }

        [Fact]
        public async Task Test_UpdateProduct()
        {
            using var db = CreateDbContext();
            var repo = new ProductRepository(db);

            String pid = RandomString(5);

            db.Products.Add(new Product
            {
                Id = pid,
                Name = "RAM 16GB",
                Description = "DDR4",
                Price = "50",
                Stock = 5
            });
            await db.SaveChangesAsync();

            await repo.UpdateProductsAsync(new Product
            {
                Id = pid,
                Name = "RAM 32GB",
                Description = "DDR4 Kit",
                Price = "90",
                Stock = 7
            });

            var updated = await db.Products.FirstAsync(p => p.Id == pid);
            Assert.Equal("RAM 32GB", updated.Name);
            Assert.Equal(7, updated.Stock);
        }



        private static readonly Random random = new();

        private static string RandomString(int length)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}

