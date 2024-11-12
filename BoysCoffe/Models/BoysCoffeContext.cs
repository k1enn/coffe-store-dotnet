using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using BoysCoffe.Models;

public class BoysCoffeContext : DbContext
{
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
        modelBuilder.Entity<User>()
            .HasOptional(u => u.Cart)
            .WithRequired(c => c.User);

        modelBuilder.Entity<Cart>()
            .HasMany(c => c.CartItems)
            .WithRequired(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId);

        modelBuilder.Entity<Category>()
            .HasMany(cat => cat.Products)
            .WithRequired(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithRequired(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.CartItems)
            .WithRequired(ci => ci.Product)
            .HasForeignKey(ci => ci.ProductId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.OrderItems)
            .WithRequired(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId);
    }
}
