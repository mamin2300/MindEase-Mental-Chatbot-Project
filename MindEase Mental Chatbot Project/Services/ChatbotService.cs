using MindEase_Mental_Chatbot_Project.Models;

namespace MindEase_Mental_Chatbot_Project.Services
{
    public class ChatbotService
    {
        private readonly AIService _aiService;

        public ChatbotService()
        {
            _aiService = new AIService();
        }

        public ChatResponse GetResponse(Message message)
        {
            return new ChatResponse
            {
                BotResponse = _aiService.GenerateResponse(message.UserMessage)
            };
        }
    }
}