namespace MindEase_Mental_Chatbot_Project.Models
{
    public class MoodEntry
    {
        public int Id { get; set; }
        public int MoodLevel { get; set; } // 1 to 5
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
