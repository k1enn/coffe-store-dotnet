using System.Collections.Generic;

namespace BoysCoffe.Models
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<Order> RecentOrders { get; set; }
        public decimal TotalSales { get; set; }
        public int ProductCount { get; set; }
        public int UserCount { get; set; }
    }
}
