using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindEase_Mental_Chatbot_Project.Data;
using MindEase_Mental_Chatbot_Project.Models;
using MindEase_Mental_Chatbot_Project.Services;
using Microsoft.AspNetCore.Authorization;

namespace MindEase_Mental_Chatbot_Project.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IChatbotService _chatbotService;
        private readonly AppDbContext _context;

        private static readonly string[] CrisisKeywords =
        {
            "suicide", "kill myself", "end my life", "want to die",
            "self harm", "hurt myself", "not worth living", "can't go on",
            "hopeless", "helpless", "crisis", "emergency"
        };

        public StudentController(IChatbotService chatbotService, AppDbContext context)
        {
            _chatbotService = chatbotService;
            _context = context;
        }

        // GET: /Student
        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name ?? "";

            var recentMoods = await _context.MoodEntries
                .Where(m => m.Username == username)
                .OrderByDescending(m => m.CreatedAt)
                .Take(3)
                .ToListAsync();

            // Personalized greeting based on average mood this week
            var weekAgo = DateTime.Now.AddDays(-7);
            var recentAvg = await _context.MoodEntries
                .Where(m => m.Username == username && m.CreatedAt >= weekAgo)
                .AverageAsync(m => (double?)m.MoodLevel);

            ViewBag.MoodAverage = recentAvg;

            // Chart data — last 7 mood entries
            var chartData = await _context.MoodEntries
                .Where(m => m.Username == username)
                .OrderByDescending(m => m.CreatedAt)
                .Take(7)
                .ToListAsync();

            chartData.Reverse();
            ViewBag.ChartLabels = chartData.Select(m => m.CreatedAt.ToString("MMM d")).ToList();
            ViewBag.ChartValues = chartData.Select(m => m.MoodLevel).ToList();

            return View(recentMoods);
        }

        // GET: /Student/Chat
        [HttpGet]
        public async Task<IActionResult> Chat()
        {
            var username = User.Identity?.Name ?? "";
            var chatHistory = await _context.ChatMessages
                .Where(c => c.Username == username)
                .OrderBy(c => c.SentAt)
                .ToListAsync();
            return View(chatHistory);
        }

        // POST: /Student/Chat
        [HttpPost]
        public async Task<IActionResult> Chat(string userMessage)
        {
            var username = User.Identity?.Name ?? "";

            if (!string.IsNullOrWhiteSpace(userMessage))
            {
                var botResponse = await _chatbotService.GetResponseAsync(userMessage);

                _context.ChatMessages.Add(new ChatMessage
                {
                    Username = username,
                    UserMessage = userMessage,
                    BotResponse = botResponse,
                    SentAt = DateTime.Now
                });

                // Crisis keyword detection
                var lower = userMessage.ToLower();
                if (CrisisKeywords.Any(k => lower.Contains(k)))
                {
                    _context.CrisisAlerts.Add(new CrisisAlert
                    {
                        StudentUsername = username,
                        TriggerSource = "Chat",
                        Message = userMessage,
                        RiskLevel = "Critical",
                        CreatedAt = DateTime.Now
                    });
                }

                await _context.SaveChangesAsync();
            }

            var chatHistory = await _context.ChatMessages
                .Where(c => c.Username == username)
                .OrderBy(c => c.SentAt)
                .ToListAsync();
            return View(chatHistory);
        }

        // POST: /Student/ClearChat
        [HttpPost]
        public async Task<IActionResult> ClearChat()
        {
            var username = User.Identity?.Name ?? "";
            var messages = _context.ChatMessages.Where(c => c.Username == username).ToList();
            _context.ChatMessages.RemoveRange(messages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Chat));
        }

        // GET: /Student/LogMood
        [HttpGet]
        public IActionResult LogMood()
        {
            return View(new MoodEntry());
        }

        // POST: /Student/LogMood
        [HttpPost]
        public async Task<IActionResult> LogMood(MoodEntry entry)
        {
            entry.Username = User.Identity?.Name ?? "";
            entry.CreatedAt = DateTime.Now;
            _context.MoodEntries.Add(entry);
            await _context.SaveChangesAsync();
            return RedirectToAction("MoodHistory");
        }

        // GET: /Student/MoodHistory
        public async Task<IActionResult> MoodHistory()
        {
            var username = User.Identity?.Name ?? "";
            var moods = await _context.MoodEntries
                .Where(m => m.Username == username)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
            return View(moods);
        }
    }
}