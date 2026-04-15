namespace MindEase_Mental_Chatbot_Project.Models
{
    public class CrisisAlert
    {
        public int Id { get; set; }
        public string StudentUsername { get; set; } = string.Empty;
        public string TriggerSource { get; set; } = string.Empty; 
        public string Message { get; set; } = string.Empty;
        public string RiskLevel { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsResolved { get; set; } = false;
    }
}