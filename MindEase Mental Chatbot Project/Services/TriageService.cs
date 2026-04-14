using MindEase_Mental_Chatbot_Project.Models;

namespace MindEase_Mental_Chatbot_Project.Services
{
    public class TriageService : ITriageService
    {
        private static readonly string[] CriticalKeywords =
        {
            "suicide", "kill myself", "end my life", "want to die",
            "self harm", "hurt myself", "not worth living", "can't go on"
        };

        private static readonly string[] HighKeywords =
        {
            "crisis", "emergency", "breakdown", "can't cope",
            "hopeless", "helpless", "desperate", "losing my mind"
        };

        private static readonly string[] MediumKeywords =
        {
            "anxious", "anxiety", "depressed", "sad", "overwhelmed",
            "worried", "stressed", "lonely", "isolated", "struggling"
        };

        public TriageResult AssessRisk(string message)
        {
            var lower = message.ToLower();

            if (CriticalKeywords.Any(k => lower.Contains(k)))
                return new TriageResult
                {
                    StudentMessage = message,
                    RiskLevel = "Critical",
                    AssessmentNotes = "Message contains indicators of immediate self-harm or suicidal ideation. Immediate professional intervention required.",
                    AssessedAt = DateTime.Now,
                    EscalatedToCrisis = true
                };

            if (HighKeywords.Any(k => lower.Contains(k)))
                return new TriageResult
                {
                    StudentMessage = message,
                    RiskLevel = "High",
                    AssessmentNotes = "Message indicates severe emotional distress. Priority referral to campus counselling is strongly recommended.",
                    AssessedAt = DateTime.Now,
                    EscalatedToCrisis = true
                };

            if (MediumKeywords.Any(k => lower.Contains(k)))
                return new TriageResult
                {
                    StudentMessage = message,
                    RiskLevel = "Medium",
                    AssessmentNotes = "Message shows moderate distress signals. Coping resources and self-care strategies provided.",
                    AssessedAt = DateTime.Now,
                    EscalatedToCrisis = false
                };

            return new TriageResult
            {
                StudentMessage = message,
                RiskLevel = "Low",
                AssessmentNotes = "No significant distress indicators detected. General wellness support provided.",
                AssessedAt = DateTime.Now,
                EscalatedToCrisis = false
            };
        }

        public bool IsCrisisLevel(string riskLevel) =>
            riskLevel == "Critical" || riskLevel == "High";
    }
}