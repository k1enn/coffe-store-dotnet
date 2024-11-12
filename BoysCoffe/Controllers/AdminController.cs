using BoysCoffe.Models;
using System.Linq;
using System.Web.Mvc;

namespace BoysCoffe.Controllers
{
    public class AdminController : Controller
    {
        private readonly BoysCoffeContext _context;

        public AdminController()
        {
            _context = new BoysCoffeContext();
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
