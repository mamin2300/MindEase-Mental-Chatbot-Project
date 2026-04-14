namespace MindEase_Mental_Chatbot_Project.Models
{
    public class TriageResult
    {
        public int Id { get; set; }
        public string StudentMessage { get; set; } = string.Empty;
        public string RiskLevel { get; set; } = "Low"; // Low, Medium, High, Critical
        public string AssessmentNotes { get; set; } = string.Empty;
        public DateTime AssessedAt { get; set; } = DateTime.Now;
        public bool EscalatedToCrisis { get; set; } = false;
    }
}