namespace MindEase_Mental_Chatbot_Project.Services
{
    public class AIService
    {
        public string GenerateResponse(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "Please enter a message.";
            }

            return "You said: " + message;
        }
    }
}