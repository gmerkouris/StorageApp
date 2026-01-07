
using Microsoft.EntityFrameworkCore;
using SrorageApp.Models;
using System.Security.Claims;

namespace SrorageApp.Data
{
    public class OrderRepository
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _http;

        public OrderRepository(AppDbContext db, IHttpContextAccessor http)
        {
            _db = db;
            _http = http;
        }

        private bool IsAdmin => _http.HttpContext?.User?.IsInRole("admin") == true;

        private string? CurrentUserId =>
            _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<List<Order>> GetOrdersWithUserAsync()
        {
            var q = _db.Orders
                .AsNoTracking()
                .Include(o => o.User)
                .AsQueryable();

            if (!IsAdmin)
            {
                var userId = CurrentUserId;
                if (string.IsNullOrWhiteSpace(userId))
                    return new List<Order>();

                q = q.Where(o => o.UserId == userId);
            }

            return await q.ToListAsync();
        }

        public async Task<bool> AddOrderAsync(Order ord)
        {
            var userId = CurrentUserId;
            if (string.IsNullOrWhiteSpace(userId))
                return false;

            ord.Id = RandomString(8);
            ord.UserId = userId; 

            await using var tx = await _db.Database.BeginTransactionAsync();

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == ord.ProductId);
            if (product == null) return false;

            if (product.Stock < ord.Quantity) return false;

            product.Stock -= ord.Quantity;
            _db.Orders.Add(ord);

            await _db.SaveChangesAsync();
            await tx.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            await using var tx = await _db.Database.BeginTransactionAsync();

            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return false;

            if (!IsAdmin)
            {
                var userId = CurrentUserId;
                if (string.IsNullOrWhiteSpace(userId) || order.UserId != userId)
                    return false;
            }

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == order.ProductId);
            if (product != null)
                product.Stock += order.Quantity;

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            await tx.CommitAsync();
            return true;
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
