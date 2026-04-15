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

        public StudentController(IChatbotService chatbotService, AppDbContext context)
        {
            _chatbotService = chatbotService;
            _context = context;
        }

        // GET: /Student
        public async Task<IActionResult> Index()
        {
            var recentMoods = await _context.MoodEntries
                .OrderByDescending(m => m.CreatedAt)
                .Take(3)
                .ToListAsync();
            return View(recentMoods);
        }

        // GET: /Student/Chat
        [HttpGet]
        public async Task<IActionResult> Chat()
        {
            var chatHistory = await _context.ChatMessages.OrderBy(c => c.SentAt).ToListAsync();
            return View(chatHistory);
        }

        // POST: /Student/Chat
        [HttpPost]
        public async Task<IActionResult> Chat(string userMessage)
        {
            if (!string.IsNullOrWhiteSpace(userMessage))
            {
                var botResponse = await _chatbotService.GetResponseAsync(userMessage);
                var chat = new ChatMessage
                {
                    UserMessage = userMessage,
                    BotResponse = botResponse,
                    SentAt = DateTime.Now
                };
                _context.ChatMessages.Add(chat);
                await _context.SaveChangesAsync();
            }
            var chatHistory = await _context.ChatMessages.OrderBy(c => c.SentAt).ToListAsync();
            return View(chatHistory);
        }

        // POST: /Student/ClearChat
        [HttpPost]
        public async Task<IActionResult> ClearChat()
        {
            var messages = _context.ChatMessages.ToList();
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
            entry.CreatedAt = DateTime.Now;
            _context.MoodEntries.Add(entry);
            await _context.SaveChangesAsync();
            return RedirectToAction("MoodHistory");
        }

        // GET: /Student/MoodHistory
        public async Task<IActionResult> MoodHistory()
        {
            var moods = await _context.MoodEntries.OrderByDescending(m => m.CreatedAt).ToListAsync();
            return View(moods);
        }
    }
}
