using Microsoft.EntityFrameworkCore;
using Service.Abstractions.Models;

namespace Service.Api.Infrasturcture;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        base.OnModelCreating(modelBuilder);
    }
}

