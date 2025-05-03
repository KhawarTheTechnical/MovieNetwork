using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieNetwork.Models;
using System.Linq;

namespace MovieNetwork.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("auth/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("auth/login")]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);
                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewData["Error"] = "Invalid username or password.";
            return View();
        }

        [HttpGet("auth/signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost("auth/signup")]
        public JsonResult Signup(string username, string email, string password, string confirmPassword, string interests)
        {
            if (password != confirmPassword)
            {
                return Json(new { success = false, message = "Passwords do not match." });
            }

            if (_context.Users.Any(u => u.Username == username))
            {
                return Json(new { success = false, message = "Username is already taken." });
            }

            if (_context.Users.Any(u => u.Email == email))
            {
                return Json(new { success = false, message = "Email is already registered." });
            }

            var passwordHasher = new PasswordHasher<User>();
            var user = new User
            {
                Username = username,
                Email = email,
                Interests = interests
            };
            user.Password = passwordHasher.HashPassword(user, password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpGet("auth/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        [HttpGet("auth/check-username")]
        public JsonResult CheckUsername(string username)
        {
            var exists = _context.Users.Any(u => u.Username == username);
            return Json(new { exists });
        }

        [HttpGet("auth/check-email")]
        public JsonResult CheckEmail(string email)
        {
            var exists = _context.Users.Any(u => u.Email == email);
            return Json(new { exists });
        }
    }
}