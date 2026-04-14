using MindEase_Mental_Chatbot_Project.Models;

namespace MindEase_Mental_Chatbot_Project.Services
{
    public interface ITriageService
    {
        TriageResult AssessRisk(string message);
        bool IsCrisisLevel(string riskLevel);
    }
}