using CoffeeShopApp.Models;
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
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                var adminUser = new User
                {
                    Username = "iamk1en",
                    Password = "kiendeptrai", // Ideally, hash this password before storing
                    Email = "dinhtrungkien05@gmail.com",
                    Role = "Admin"
                };
                var user = context.Users.FirstOrDefault();
                if (user != null)
                {
                    context.Orders.AddOrUpdate(
                        o => o.OrderId,
                        new Order
                        {
                            OrderDate = DateTime.Now,
                            UserId = user.UserId,
                            TotalPrice = 50m,
                            OrderItems = new List<OrderItem>
                            {
                            new OrderItem { ProductId = 1, Quantity = 2, TotalPrice = 25m },
                            new OrderItem { ProductId = 2, Quantity = 1, TotalPrice = 25m }
                            }
                        },
                        new Order
                        {
                            OrderDate = DateTime.Now.AddDays(-1),
                            UserId = user.UserId,
                            TotalPrice = 30m,
                            OrderItems = new List<OrderItem>
                            {
                            new OrderItem { ProductId = 3, Quantity = 3, TotalPrice = 30m }
                            }
                        }
                    );

                    context.Users.Add(adminUser);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error saving default admin user: {ex.Message}");
                    }
                }
            }
        }
    }
}
