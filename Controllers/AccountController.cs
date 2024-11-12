using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;
using CoffeeShopApp.Models;
using Expense_Tracker_App.Migrations;
using CoffeeShopApp.Data;

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
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không được để trống.");
                return View();
            }

            // Check login credentials
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Store user ID and role in session
                Session["UserId"] = user.UserId;
                Session["Role"] = user.Role;

                // Redirect based on user role
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Products"); // Adjust action and controller names if necessary
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
