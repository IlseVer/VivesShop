using Microsoft.EntityFrameworkCore;
using VivesShop.UI.Mvc.Models;

namespace VivesShop.UI.Mvc.Core
{
    public class VivesShopDbContext(DbContextOptions<VivesShopDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Shop => Set<Product>();
        public DbSet<OrderItem> Orders => Set<OrderItem>();

        public void Seed()
        {
            var shop = new List<Product>
            {
                new Product { Name = "Medium friet", Price = 3 },
                new Product { Name = "Frikandel", Price = 2 },
                new Product { Name = "Cola Zero", Price = 2 },
                new Product { Name = "Water", Price = 1.5 },
                new Product { Name = "Mayonaise", Price = 0.5 }
            };

            Shop.AddRange(shop);

            SaveChanges();

        }
    }
}
