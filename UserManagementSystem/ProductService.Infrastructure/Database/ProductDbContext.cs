using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Database
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>()
                        .Property(p => p.ProductId)
                        .ValueGeneratedOnAdd();
        }
    }
}
