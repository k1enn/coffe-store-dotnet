using BoysCoffe.Models;
using System.Linq;
using System.Web.Mvc;


namespace BoysCoffe.Controllers
{
    public class AccountController : Controller
    {
        private readonly BoysCoffeContext _context;

        public AccountController()
        {
            _context = new BoysCoffeContext();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RoleOptions = new SelectList(new[] { "Admin", "Customer" });
            return View();
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirect to an appropriate page after creating
            }

            ViewBag.RoleOptions = new SelectList(new[] { "Admin", "Customer" });
            return View(model);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không được để trống.");
                return View();
            }

            // Kiểm tra thông tin đăng nhập
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Lưu thông tin người dùng vào session
                Session["UserId"] = user.UserId;
                Session["Role"] = user.Role;

                // Nếu returnUrl tồn tại, chuyển hướng về đó, nếu không thì chuyển hướng mặc định
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl); // Chuyển hướng về trang Checkout hoặc trang ban đầu yêu cầu
                }

                // Chuyển hướng mặc định dựa trên vai trò người dùng nếu không có returnUrl
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Thông tin đăng nhập không đúng.");
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear(); // Clear all session data
            return RedirectToAction("Index", "Home"); // Return home page
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Password confirmation check
                if (model.Password != confirmPassword)
                {
                    ModelState.AddModelError("confirmPassword", "Mật khẩu xác nhận không khớp.");
                    return View(model);
                }

                // Check if username already exists
                if (_context.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Tên người dùng đã tồn tại.");
                    return View(model);
                }

                // Set default role for new user as Customer
                model.Role = "Customer";

                // Add new user to the database
                _context.Users.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }
    }
}
