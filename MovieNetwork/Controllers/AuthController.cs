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
        public IActionResult Signup(string username, string email, string password, string confirmPassword, string interests)
        {
            if (password != confirmPassword)
            {
                ViewData["ErrorMessage"] = "Passwords do not match.";
                return RedirectToAction("Signup");
            }

            if (_context.Users.Any(u => u.Username == username))
            {
                ViewData["ErrorMessage"] = "Username is already taken.";
                return RedirectToAction("Signup");
            }

            if (_context.Users.Any(u => u.Email == email))
            {
                ViewData["ErrorMessage"] = "Email is already registered.";
                return RedirectToAction("Signup");
            }

            var passwordHasher = new PasswordHasher<User>();
            var user = new User
            {
                Username = username,
                Email = email,
                Interests = interests,
                Password = passwordHasher.HashPassword(null, password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            ViewData["SuccessMessage"] = "Signup successful! You can now log in.";
            return RedirectToAction("Login");
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