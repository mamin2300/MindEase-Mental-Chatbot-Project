
// Author: Malika
// Handles admin authentication and redirects to analytics dashboard.

using Microsoft.AspNetCore.Mvc;

namespace MindEase_Mental_Chatbot_Project.Controllers
{
    public class AdminController : Controller
    {
        // Hardcoded admin credentials for demo purposes
        private const string AdminUsername = "admin";
        private const string AdminPassword = "mindease123";

        // GET: Admin/Login - shows the admin login form
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login - validates credentials and redirects to dashboard
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Check if credentials match hardcoded admin credentials
            if (username == AdminUsername && password == AdminPassword)
            {
                // Store admin session flag
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Dashboard", "Analytics");
            }

            // Invalid credentials - return error
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // GET: Admin/Logout - clears session and redirects to login
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}