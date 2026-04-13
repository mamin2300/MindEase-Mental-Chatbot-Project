using Microsoft.AspNetCore.Mvc;
using MindEase_Mental_Chatbot_Project.Models;
using MindEase_Mental_Chatbot_Project.Services;

namespace MindEase_Mental_Chatbot_Project.Controllers
{
    public class StudentController : Controller
    {
        private readonly IChatbotService _chatbotService;

        // In-memory dummy data (no database)
        private static List<MoodEntry> _moods = new List<MoodEntry>
        {
            new MoodEntry { Id = 1, MoodLevel = 4, Notes = "Feeling good today!", CreatedAt = DateTime.Now.AddDays(-2) },
            new MoodEntry { Id = 2, MoodLevel = 2, Notes = "Stressed about exams", CreatedAt = DateTime.Now.AddDays(-1) },
            new MoodEntry { Id = 3, MoodLevel = 5, Notes = "Had a great day with friends", CreatedAt = DateTime.Now }
        };

        private static List<ChatMessage> _chatHistory = new List<ChatMessage>();

        public StudentController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        // GET: /Student
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Student/Chat
        [HttpGet]
        public IActionResult Chat()
        {
            return View(_chatHistory);
        }

        // POST: /Student/Chat
        [HttpPost]
        public IActionResult Chat(string userMessage)
        {
            if (!string.IsNullOrWhiteSpace(userMessage))
            {
                var botResponse = _chatbotService.GetResponse(userMessage);
                var chat = new ChatMessage
                {
                    Id = _chatHistory.Count + 1,
                    UserMessage = userMessage,
                    BotResponse = botResponse,
                    SentAt = DateTime.Now
                };
                _chatHistory.Add(chat);
            }
            return View(_chatHistory);
        }

        // GET: /Student/LogMood
        [HttpGet]
        public IActionResult LogMood()
        {
            return View(new MoodEntry());
        }

        // POST: /Student/LogMood
        [HttpPost]
        public IActionResult LogMood(MoodEntry entry)
        {
            entry.Id = _moods.Count + 1;
            entry.CreatedAt = DateTime.Now;
            _moods.Add(entry);
            return RedirectToAction("MoodHistory");
        }

        // GET: /Student/MoodHistory
        public IActionResult MoodHistory()
        {
            return View(_moods.OrderByDescending(m => m.CreatedAt).ToList());
        }
    }
}
