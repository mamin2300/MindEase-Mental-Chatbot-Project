namespace MindEase_Mental_Chatbot_Project.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Student";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}