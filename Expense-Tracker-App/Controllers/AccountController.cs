using System.Linq;
using System.Web.Mvc;
using CoffeeShopApp.Models;

namespace CoffeeShopApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly CoffeeShopContext _context;

        public AccountController()
        {
            _context = new CoffeeShopContext();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không được để trống.");
                return View();
            }

            // Kiểm tra thông tin đăng nhập
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Đăng nhập thành công, có thể tạo session hoặc cookie
                Session["UserId"] = user.UserId; // lưu UserId vào session
                return RedirectToAction("Index", "Home"); // chuyển hướng về trang chính
            }

            ModelState.AddModelError("", "Thông tin đăng nhập không đúng.");
            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon(); // Xóa session
            return RedirectToAction("Login", "Account");

        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model, string confirmPassword)
        {
            if (ModelState.IsValid)
            {


                // Kiểm tra tên người dùng đã tồn tại
                if (_context.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Tên người dùng đã tồn tại.");
                    return View(model);
                }
                // Lưu người dùng mới vào database
                _context.Users.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(model);
        }


    }
}
