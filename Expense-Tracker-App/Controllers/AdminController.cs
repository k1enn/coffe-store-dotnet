using System;
using System.Linq;
using System.Web.Mvc;
using CoffeeShopApp.Models;
using CoffeeShopApp.Data;

namespace CoffeeShopApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly CoffeeShopContext _context;

        public AdminController()
        {
            _context = new CoffeeShopContext();
        }

        // GET: Admin/Dashboard
        public ActionResult Dashboard()
        {
            var recentOrders = _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToList();

            var totalSales = _context.Orders.Sum(o => o.TotalPrice);
            var productCount = _context.Products.Count();
            var userCount = _context.Users.Count();

            var dashboardData = new AdminDashboardViewModel
            {
                RecentOrders = recentOrders,
                TotalSales = totalSales,
                ProductCount = productCount,
                UserCount = userCount
            };

            return View(dashboardData);
        }

        
       
    }
}
