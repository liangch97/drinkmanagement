using Microsoft.EntityFrameworkCore;
using DrinkManagement.Models;

namespace DrinkManagement.Data;

public class DrinkDbContext : DbContext
{
    public DrinkDbContext(DbContextOptions<DrinkDbContext> options) : base(options)
    {
    }

    public DbSet<Drink> Drinks { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Drink entity
        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Drinks)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Category entity
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        // Seed initial data
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "茶饮", Description = "各类茶饮料" },
            new Category { Id = 2, Name = "咖啡", Description = "咖啡类饮品" },
            new Category { Id = 3, Name = "果汁", Description = "鲜榨果汁" },
            new Category { Id = 4, Name = "奶茶", Description = "奶茶系列" }
        );

        modelBuilder.Entity<Drink>().HasData(
            new Drink { Id = 1, Name = "绿茶", Description = "清新绿茶", Price = 8.00m, CategoryId = 1, Stock = 100 },
            new Drink { Id = 2, Name = "红茶", Description = "经典红茶", Price = 8.00m, CategoryId = 1, Stock = 100 },
            new Drink { Id = 3, Name = "美式咖啡", Description = "浓郁美式", Price = 15.00m, CategoryId = 2, Stock = 80 },
            new Drink { Id = 4, Name = "拿铁", Description = "香醇拿铁", Price = 18.00m, CategoryId = 2, Stock = 80 },
            new Drink { Id = 5, Name = "橙汁", Description = "鲜榨橙汁", Price = 12.00m, CategoryId = 3, Stock = 60 },
            new Drink { Id = 6, Name = "珍珠奶茶", Description = "经典珍珠奶茶", Price = 10.00m, CategoryId = 4, Stock = 90 }
        );
    }
}
