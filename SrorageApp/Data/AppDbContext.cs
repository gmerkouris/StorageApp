using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SrorageApp.Models;

namespace SrorageApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(e =>
            {
                e.ToTable("Product", "dbo");
                e.HasKey(p => p.Id);

                e.Property(p => p.Id).HasColumnName("id").HasMaxLength(450);
                e.Property(p => p.Name).HasColumnName("name").HasMaxLength(450);
                e.Property(p => p.Description).HasColumnName("description").HasMaxLength(450);
                e.Property(p => p.Price).HasColumnName("price").HasMaxLength(450); 
                e.Property(p => p.Stock).HasColumnName("stock");
            });

            builder.Entity<Order>(e =>
            {
                e.ToTable("Order", "dbo");
                e.HasKey(o => o.Id);

                e.Property(o => o.Id).HasColumnName("id").HasMaxLength(450);
                e.Property(o => o.UserId).HasColumnName("userid").HasMaxLength(450).IsRequired();
                e.Property(o => o.ProductId).HasColumnName("productid").HasMaxLength(450);
                e.Property(o => o.Quantity).HasColumnName("quantity").IsRequired();
                e.Property(o => o.Address).HasColumnName("address").HasMaxLength(450);
                e.Property(o => o.Contact).HasColumnName("contact").HasMaxLength(450);
                e.Property(o => o.FullName).HasColumnName("fullName").HasMaxLength(450);

             
                e.HasOne(o => o.Product)
                 .WithMany()
                 .HasForeignKey(o => o.ProductId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(o => o.User)
                 .WithMany()
                 .HasForeignKey(o => o.UserId)
                 .OnDelete(DeleteBehavior.NoAction);


            });
        }
    }
}

