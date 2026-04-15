using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MindEase_Mental_Chatbot_Project.Data;
using MindEase_Mental_Chatbot_Project.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MindEase_Mental_Chatbot_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ── Helpers ──────────────────────────────────────
        private static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        private async Task SignInUser(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Student")
            };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(identity));
        }

        // ── Login ─────────────────────────────────────────
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Student");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var hashed = HashPassword(password);
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.PasswordHash == hashed);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            await SignInUser(user.Username);
            return RedirectToAction("Index", "Student");
        }

        // ── Register ──────────────────────────────────────
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Student");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "That username is already taken.";
                return View();
            }

            var user = new ApplicationUser
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Role = "Student",
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Log them in immediately after registering
            await SignInUser(user.Username);
            return RedirectToAction("Index", "Student");
        }

        // ── Logout ────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }
    }
}