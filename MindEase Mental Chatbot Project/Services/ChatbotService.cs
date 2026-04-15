namespace MindEase_Mental_Chatbot_Project.Services
{
    public class ChatbotService : IChatbotService
    {
            // Async wrapper — used as fallback if Azure is unavailable
        public Task<string> GetResponseAsync(string message) =>
                Task.FromResult(GetResponse(message));
        public string GetResponse(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return "Please enter a message.";

            var msg = message.ToLower();

            if (msg.Contains("sad") || msg.Contains("depressed") || msg.Contains("unhappy"))
                return "I'm sorry you're feeling this way. Remember, it's okay to not be okay. Consider talking to a counselor if these feelings persist.";

            if (msg.Contains("anxious") || msg.Contains("anxiety") || msg.Contains("worried") || msg.Contains("stress"))
                return "Take a deep breath. Try the 4-7-8 breathing technique: breathe in for 4 seconds, hold for 7, exhale for 8. You've got this!";

            if (msg.Contains("happy") || msg.Contains("great") || msg.Contains("good") || msg.Contains("wonderful"))
                return "That's wonderful to hear! Keep doing what makes you feel good. Positive energy is contagious!";

            if (msg.Contains("angry") || msg.Contains("frustrated") || msg.Contains("mad"))
                return "It's natural to feel angry sometimes. Try stepping away for a moment, take some deep breaths, and revisit the situation when you feel calmer.";

            if (msg.Contains("lonely") || msg.Contains("alone") || msg.Contains("isolated"))
                return "Feeling lonely can be really tough. Consider reaching out to a friend, joining a campus club, or visiting the student wellness center.";

            if (msg.Contains("sleep") || msg.Contains("tired") || msg.Contains("insomnia"))
                return "Sleep is crucial for mental health. Try to maintain a consistent sleep schedule and avoid screens before bed.";

            if (msg.Contains("help") || msg.Contains("crisis") || msg.Contains("emergency"))
                return "If you're in crisis, please contact the campus counseling center or call a mental health helpline immediately. You are not alone.";

            if (msg.Contains("hello") || msg.Contains("hi") || msg.Contains("hey"))
                return "Hello! I'm MindEase, your mental wellness companion. How are you feeling today?";

            return "Thank you for sharing. I'm here to listen. Can you tell me more about how you're feeling?";
        }
    }    
}
