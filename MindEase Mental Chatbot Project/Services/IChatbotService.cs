namespace MindEase_Mental_Chatbot_Project.Services
{
    public interface IChatbotService
    {
        string GetResponse(string message);
        Task<string> GetResponseAsync(string message);
    }
}