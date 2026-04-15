// Author: Malika
// Handles admin authentication and redirects to analytics dashboard.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindEase_Mental_Chatbot_Project.Data;

namespace MindEase_Mental_Chatbot_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        // Hardcoded admin credentials for demo purposes
        private const string AdminUsername = "admin";
        private const string AdminPassword = "mindease123";

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Login - shows the admin login form
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login - validates credentials and redirects to dashboard
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Dashboard", "Analytics");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // GET: Admin/CrisisAlerts
        public async Task<IActionResult> CrisisAlerts()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var alerts = await _context.CrisisAlerts
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return View(alerts);
        }

        // POST: Admin/ResolveAlert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResolveAlert(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var alert = await _context.CrisisAlerts.FindAsync(id);
            if (alert != null)
            {
                alert.IsResolved = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(CrisisAlerts));
        }

        // GET: Admin/Logout - clears session and redirects to login
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}