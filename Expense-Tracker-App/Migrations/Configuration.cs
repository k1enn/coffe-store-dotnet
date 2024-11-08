using CoffeeShopApp.Models;
namespace Expense_Tracker_App.Migrations
{
    using System;
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

                context.Users.Add(adminUser);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex) {
                
                    Console.WriteLine($"Error saving default admin user: {ex.Message}");
                }
            }
        }
    }
}
