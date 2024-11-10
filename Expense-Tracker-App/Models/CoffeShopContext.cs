using System.Data.Entity;

namespace CoffeeShopApp.Models
{
        public class CoffeeShopContext : DbContext
        {
            public CoffeeShopContext() : base("name=CoffeeShopContext") { }

            public DbSet<User> Users { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<CartItem> CartItems { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderItem> OrderItems { get; set; }
            public DbSet<LoginHistory> LoginHistories { get; set; }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Category>()
                    .HasMany(c => c.Products)
                    .WithRequired(p => p.Category)
                    .HasForeignKey(p => p.CategoryId);

                modelBuilder.Entity<Cart>()
                    .HasMany(c => c.CartItems)
                    .WithRequired(ci => ci.Cart)
                    .HasForeignKey(ci => ci.CartId);

                modelBuilder.Entity<Order>()
                    .HasMany(o => o.OrderItems)
                    .WithRequired(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId);

                base.OnModelCreating(modelBuilder);
            }
        }

}
