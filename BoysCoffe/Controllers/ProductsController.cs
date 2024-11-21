using BoysCoffe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using BoysCoffe.Custom_Attribute;



namespace BoysCoffe.Controllers
{
    public class ProductsController : Controller
    {
        private BoysCoffeContext db = new BoysCoffeContext();


        // GET: Products
        [AdminAuthorize]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        public ActionResult Shopping(int? categoryId, string searchTerm)
        {
            var products = db.Products.AsQueryable();

            // Nếu có chọn danh mục
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            // Nếu có từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm));
            }

            // Lấy danh sách các danh mục sản phẩm để hiển thị trong dropdown
            ViewBag.Categories = db.Categories.ToList();

            // Trả về danh sách sản phẩm theo các tiêu chí lọc
            return View(products.ToList());
        }
        public JsonResult SearchAutocomplete(string term)
        {
            var products = db.Products
                .Where(p => p.Name.Contains(term)) // Tìm kiếm sản phẩm theo tên
                .Select(p => new { label = p.Name, value = p.Name }) // Chỉ trả về tên sản phẩm
                .Take(10) // Giới hạn số lượng kết quả gợi ý
                .ToList();

            return Json(products, JsonRequestBehavior.AllowGet); // Trả về dữ liệu dạng JSON
        }

        public ActionResult AddToCart(int id)
        {
            // Get the product by its ID
            var product = db.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return HttpNotFound();

            // Get the current cart from the session, or create a new one if it doesn't exist
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }

            // Check if the product already exists in the cart
            var existingItem = cart.FirstOrDefault(ci => ci.Product.ProductId == id);
            if (existingItem == null)
            {
                // Add new item to cart
                cart.Add(new CartItem { Product = product, Quantity = 1, Price = product.Price });
            }
            else
            {
                // Update quantity and total price
                existingItem.Quantity++;
                existingItem.Price = existingItem.Quantity * product.Price;
            }

            return RedirectToAction("ShowCart", "Products");
        }
        private List<CartItem> GetCart()
        {
            // For the sake of example, let's assume we're using session to store the cart
            return Session["Cart"] as List<CartItem>;
        }
        public ActionResult Checkout()
        {
            // Kiểm tra nếu người dùng chưa đăng nhập
            if (Session["UserId"] == null)
            {
                // Chuyển hướng đến trang đăng nhập với ReturnUrl là Checkout
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Checkout", "Products") });
            }

            var cart = GetCart(); // Lấy giỏ hàng của người dùng

            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Products"); // Chuyển hướng nếu giỏ hàng trống
            }

            // Lấy ID của người dùng đã đăng nhập từ Session
            int userId = (int)Session["UserId"];

            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalPrice = cart.Sum(c => c.TotalPrice),
                UserId = userId
            };

            db.Orders.Add(order);
            db.SaveChanges();

            // Thêm các OrderItem vào database
            foreach (var item in cart)
            {
                var product = db.Products.FirstOrDefault(p => p.ProductId == item.Product.ProductId);
                if (product == null)
                {
                    return RedirectToAction("Index", "Products");
                }

                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                };

                db.OrderItems.Add(orderItem);
            }

            db.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        public int GetCurrentUserId() => Session["UserId"] != null ? (int)Session["UserId"] : 0;

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                return RedirectToAction("ShowCart");
            }

            // Tìm sản phẩm trong giỏ hàng
            var item = cart.FirstOrDefault(ci => ci.Product.ProductId == id);

            if (item != null)
            {
                if (item.Quantity <= 1)
                {
                    // Nếu Quantity < 1, xóa sản phẩm khỏi giỏ hàng
                    cart.Remove(item);
                }
                else
                {
                    // Nếu Quantity >= 1, giảm số lượng sản phẩm đi 1
                    item.Quantity -= 1;
                }
            }

            // Sau khi xử lý, quay lại trang giỏ hàng
            return RedirectToAction("ShowCart");
        }

        public ActionResult ShowCart()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || cart.Count == 0)
            {
                return View("EmptyCart"); // Show empty cart view if no items
            }

            return View(cart);
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [AdminAuthorize]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }
        public ActionResult OrderConfirmation(int orderId)
        {
            // Fetch the order with its associated items and product details
            var order = db.Orders
                .Include(o => o.OrderItems.Select(oi => oi.Product)) // Include order items and associated products
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return HttpNotFound(); // If the order doesn't exist, return a 404 error
            }

            return View(order); // Pass the order to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult Create([Bind(Include = "ProductId,Name,Description,Price,CategoryId")] Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    // Lấy tên file và lưu nó vào thư mục images trên server
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                    imageFile.SaveAs(filePath);

                    // Lưu đường dẫn hình ảnh vào property ImageUrl
                    product.ImageUrl = "/Content/images/" + fileName;
                }
                else
                {
                    // Nếu không có hình ảnh, bạn có thể để giá trị mặc định hoặc thông báo lỗi
                    ModelState.AddModelError("", "Vui lòng chọn hình ảnh cho sản phẩm.");
                    return View(product);
                }

                // Lưu sản phẩm vào cơ sở dữ liệu
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            // Nếu model không hợp lệ, trở lại view
            return View(product);
        }
        // GET: Products/Edit/5
        [AdminAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Description,ImageUrl,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        [AdminAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [AdminAuthorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
