
// Model for displaying error information in the error view.
namespace MindEase_Mental_Chatbot_Project.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}