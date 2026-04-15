namespace MindEase_Mental_Chatbot_Project.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string UserMessage { get; set; } = string.Empty;
        public string BotResponse { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}