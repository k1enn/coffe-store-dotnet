using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShopApp.Models
{
    public class ShoppingController : Controller
    {
        private readonly CoffeeShopContext _context;

        public ShoppingController(CoffeeShopContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách sản phẩm
        public ActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }

        // Hiển thị chi tiết sản phẩm
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            // Lấy thông tin giỏ hàng của người dùng hiện tại
            var cart = GetOrCreateCart();

            // Tìm sản phẩm cần thêm
            var product = _context.Products.Find(id);

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == id);
            if (cartItem == null)
            {
                // Nếu chưa có, tạo mới một CartItem
                cartItem = new CartItem
                {
                    Cart = cart,
                    Product = product,
                    Quantity = 1
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                // Nếu đã có, tăng số lượng lên
                cartItem.Quantity++;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Hiển thị giỏ hàng
        public ActionResult Cart()
        {
            var cart = GetOrCreateCart();
            return View(cart);
        }

        // ... Các action khác như:
        // - Xóa sản phẩm khỏi giỏ hàng
        // - Cập nhật số lượng sản phẩm trong giỏ hàng
        // - Thanh toán
        // - Hiển thị lịch sử đơn hàng

        private Cart GetOrCreateCart()
        {
            // Lấy thông tin người dùng hiện tại (cần thực hiện xác thực)
            var userId = GetCurrentUserId();

            // Tìm hoặc tạo mới giỏ hàng của người dùng
            var cart = _context.Carts.FirstOrDefault(c => c.User.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            return cart;
        }
        private int GetCurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = (int)Session["UserId"]; // Store userId in a variable
                return userId;
            }
            return 0; // Or handle unauthorized access appropriately
        }
    }
}
