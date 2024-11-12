namespace BoysCoffe.Migrations
{
    using BoysCoffe.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BoysCoffeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BoysCoffeContext context)
        {
            // Add categories
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "CAFÉ" },
                new Category { CategoryId = 2, Name = "MATCHA" },
                new Category { CategoryId = 3, Name = "SINH TỐ" },
                new Category { CategoryId = 4, Name = "TRÀ" }
            };
            categories.ForEach(c => context.Categories.AddOrUpdate(x => x.CategoryId, c));

            // Define products and assign them to relevant categories
            var products = new List<Product>
            {
                // CAFÉ products
                new Product { ProductId = 1, Name = "Cà phê muối", Description = "Giống chú Long nhưng ngon hơn", Price = 25, CategoryId = 1, ImageUrl = "/coffee/muoi.jpg" },
                new Product { ProductId = 2, Name = "Cappucino", Description = "Love foam sữa mịn màng, cân bằng giữa ngọt béo hoà quyện cùng cà phê, tạo hương vị tinh tế lôi cuốn", Price = 30, CategoryId = 1, ImageUrl = "/coffee/cappucino.jpg" },
                new Product { ProductId = 3, Name = "Cafe Đen", Description = "Cà phê đen đậm vị, thơm nồng, đánh thức mọi giác quan, tỉnh táo suốt ngày dài", Price = 20, CategoryId = 1, ImageUrl = "/coffee/den.jpg" },
                new Product { ProductId = 4, Name = "Cà phê sữa", Description = "Cà phê sữa béo ngậy, ngọt vừa, đánh thức năng lượng tức thì, dành cho người cần khởi đầu nhẹ nhàng", Price = 22, CategoryId = 1, ImageUrl = "/coffee/sua.jpg" },

                // MATCHA products
                new Product { ProductId = 5, Name = "Matcha sữa", Description = "Vị trà xanh nguyên chất hoà cùng sữa mềm mịn, mang lại cảm giác nhẹ nhàng và thư thái", Price = 28, CategoryId = 2, ImageUrl = "/matcha/sua-tuoi.jpg" },
                new Product { ProductId = 6, Name = "Matcha café sữa tươi", Description = "Gấp đôi sự tỉnh táo cùng với hương vị phức tạp được tạo ra từ cà phê và trà xanh", Price = 35, CategoryId = 2, ImageUrl = "/matcha/cafe-sua.jpg" },

                // SINH TỐ products
                new Product { ProductId = 7, Name = "Sinh tố xoài", Description = "", Price = 40, CategoryId = 3, ImageUrl = "/smoothie/xoai.jpg" },
                new Product { ProductId = 8, Name = "Sinh tố chanh dây", Description = "", Price = 38, CategoryId = 3, ImageUrl = "/smoothie/chanh-day.jpg" },
                new Product { ProductId = 9, Name = "Sinh tố đào", Description = "", Price = 39, CategoryId = 3, ImageUrl = "/smoothie/dao.jpg" },

                // TRÀ products
                new Product { ProductId = 10, Name = "Trà Cam Bưởi", Description = "Vị bưởi thơm nồng, quyện với cam tươi mát, mang lại sự tươi mới, ngọt ngào tự nhiên", Price = 32, CategoryId = 4, ImageUrl = "/tea/cam-buoi.jpg" },
                new Product { ProductId = 11, Name = "Trà Bá Tước Táo Quế", Description = "Chua - ngọt - đắng nhẹ - lưu lại từng note hương trên môi bạn", Price = 34, CategoryId = 4, ImageUrl = "/tea/tao-que.jpg" },
                new Product { ProductId = 12, Name = "Trà vải hoa hồng", Description = "Trà của hội yêu cái đẹp, thanh mát, thơm ngọt, uống vào là “tâm hồn nở hoa”... thấy mình cũng xinh tươi như một cánh hồng", Price = 36, CategoryId = 4, ImageUrl = "/tea/vai-hoa-hong.jpg" },
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
