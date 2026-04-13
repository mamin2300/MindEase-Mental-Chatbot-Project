using Microsoft.AspNetCore.Mvc;
using MindEase_Mental_Chatbot_Project.Models;
using MindEase_Mental_Chatbot_Project.Services;

namespace MindEase_Mental_Chatbot_Project.Controllers
{
    public class ChatbotController : Controller
    {
        private readonly ChatbotService _chatbotService;

        public ChatbotController()
        {
            _chatbotService = new ChatbotService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Message());
        }

        [HttpPost]
        public IActionResult Index(Message message)
        {
            var response = _chatbotService.GetResponse(message);
            ViewBag.BotResponse = response.BotResponse;
            return View(message);
        }
    }
}