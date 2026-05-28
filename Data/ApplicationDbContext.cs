using Microsoft.EntityFrameworkCore;
using ProductApplication.Models;

namespace ProductApplication.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed default values
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Default Product", Price = 100 },
            new Product { Id = 2, Name = "Sample Product", Price = 200 }
        );
    }
}