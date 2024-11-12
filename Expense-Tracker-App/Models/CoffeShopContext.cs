using System.Data.Entity;
using CoffeeShopApp.Models;
using CoffeeShopApp.Controllers;
using Expense_Tracker_App.ViewModel;

namespace CoffeeShopApp.Data
{
    public class CoffeeShopContext : DbContext
    {
        public CoffeeShopContext() : base("name=CoffeeShopContext") // Ensure connection string matches this name in Web.config
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Define primary keys and foreign keys for each model

            modelBuilder.Entity<User>()
                .HasOptional(u => u.Cart)
                .WithRequired(c => c.User);


            // CartItem and Cart - One-to-Many Relationship
            modelBuilder.Entity<CartItem>()
                .HasRequired(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            // CartItem and Product - One-to-Many Relationship
            modelBuilder.Entity<CartItem>()
                .HasRequired(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);

            // Order and User - Many-to-One Relationship
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // OrderItem and Order - One-to-Many Relationship
            modelBuilder.Entity<OrderItem>()
                .HasRequired(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            // OrderItem and Product - One-to-Many Relationship
            modelBuilder.Entity<OrderItem>()
                .HasRequired(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            // Category and Product - One-to-Many Relationship
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Define any additional configurations if necessary

            base.OnModelCreating(modelBuilder);
        }
    }
}
