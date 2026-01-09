using Microsoft.EntityFrameworkCore;
using StorageApp.Models;

namespace StorageApp.Data
{
    public class ProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public Task<List<Product>> GetProductsAsync()
            => _db.Products.AsNoTracking().ToListAsync();

        public Task<Product?> FindByIdAsync(string id)
            => _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddProductsAsync(Product pr)
        {
            _db.Products.Add(pr);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateProductsAsync(Product pr)
        {
            var existing = await _db.Products.FindAsync(pr.Id);
            if (existing == null) return;

            existing.Name = pr.Name;
            existing.Description = pr.Description;
            existing.Price = pr.Price;
            existing.Stock = pr.Stock;

            await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var hasOrders = await _db.Orders.AnyAsync(o => o.ProductId == id);
            if (hasOrders) return false;

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return false;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
