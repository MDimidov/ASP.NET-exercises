using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Data.Models;

namespace ShoppingListApp.Data
{
    public class ShoppingListDbContext : DbContext
    {
        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Ham", Price = 20.35m },
                new Product() { Id = 2, Name = "Salami", Price = 5.0m },
                new Product() { Id = 3, Name = "Chicken", Price = 8.38m },
                new Product() { Id = 4, Name = "Chocolate", Price = 5.23m }
                );
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductNote> ProductNotes { get; set; }

    }
}
