using CoffeeShopApp.Models;
using CoffeeShopApp.Data;
namespace Expense_Tracker_App.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoffeeShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CoffeeShopContext context)
        {
            // Add categories
            // Define categories
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Coffee" },
                new Category { CategoryId = 2, Name = "Tea" },
                new Category { CategoryId = 3, Name = "Smoothie" },
                new Category { CategoryId = 4, Name = "Soda"},
                // Add more categories as needed
            };
            categories.ForEach(c => context.Categories.AddOrUpdate(x => x.CategoryId, c));

            // Define products and assign them to relevant categories
            var products = new List<Product>
            {
                // Coffee products
                new Product { ProductId = 1, Name = "Cà phê đen", Price = 25, CategoryId = 1, ImageUrl = "/images/ca-phe-den.jpg" },
                new Product { ProductId = 2, Name = "Cà phê sữa đá", Price = 30, CategoryId = 1, ImageUrl = "/images/ca-phe-sua-da.jpg" },
                new Product { ProductId = 3, Name = "Cà phê trứng", Price = 35, CategoryId = 1, ImageUrl = "/images/ca-phe-trung.jpg" },

                // Tea products
                new Product { ProductId = 4, Name = "Trà đào cam sả", Price = 28, CategoryId = 2, ImageUrl = "/images/tra-dao-cam-sa.jpg" },
                new Product { ProductId = 5, Name = "Trà sữa", Price = 30, CategoryId = 2, ImageUrl = "/images/tra-sua.jpg" },
                new Product { ProductId = 6, Name = "Trà chanh", Price = 22, CategoryId = 2, ImageUrl = "/images/tra-chanh.jpg" },
                new Product { ProductId = 7, Name = "Trà dâu tằm", Price = 25, CategoryId = 2, ImageUrl = "/images/tra-dau-tam.jpg" },

                // Smoothie products
                new Product { ProductId = 8, Name = "Sinh tố bơ", Price = 40, CategoryId = 3, ImageUrl = "/images/sinh-to-bo.jpg" },
                new Product { ProductId = 9, Name = "Sinh tố xoài", Price = 40, CategoryId = 3, ImageUrl = "/images/sinh-to-xoai.jpg" },
                new Product { ProductId = 10, Name = "Chanh tuyết", Price = 30, CategoryId = 3, ImageUrl = "/images/chanh-tuyet.jpg" },
                new Product { ProductId = 16, Name = "Matcha đá xay", Price = 35, CategoryId = 3, ImageUrl = "/images/matcha-da-xay.jpg" },
                
                // Soda
                new Product { ProductId = 11, Name = "Soda chanh dây", Price = 32, CategoryId = 4, ImageUrl = "/images/soda-chanh-day.jpg" },
                new Product { ProductId = 11, Name = "Soda việt quất", Price = 34, CategoryId = 4, ImageUrl = "/images/soda-viet-quat.jpg" },

                // Other beverages
                new Product { ProductId = 13, Name = "Nước ép cam", Price = 30, CategoryId = 3, ImageUrl = "/images/nuoc-ep-cam.jpg" },
                new Product { ProductId = 14, Name = "Nước ép dứa", Price = 30, CategoryId = 3, ImageUrl = "/images/nuoc-ep-dua.jpg" },
                new Product { ProductId = 17, Name = "Nước dừa", Price = 28, CategoryId = 3, ImageUrl = "/images/nuoc-dua.jpg" },
                new Product { ProductId = 18, Name = "Sữa chua đánh đá", Price = 32, CategoryId = 3, ImageUrl = "/images/sua-chua-danh-da.jpg" },
            };
            products.ForEach(p => context.Products.AddOrUpdate(x => x.ProductId, p));


            // Add users
            var users = new List<User>
            {
                new User { UserId = 1, Username = "admin123", Password = "Admin@123", Role = "Admin" },
                new User { UserId = 2, Username = "customer123", Password = "Customer@123", Role = "Customer" },
                // More users if needed
            };
            users.ForEach(u => context.Users.AddOrUpdate(x => x.UserId, u));

            // Commit all changes
            context.SaveChanges();
        }
    }    
}
