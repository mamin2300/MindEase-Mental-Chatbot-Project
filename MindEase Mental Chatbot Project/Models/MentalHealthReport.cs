namespace MindEase_Mental_Chatbot_Project.Models


// Author: Malika
// Represents a generated mental health report for campus wellness administration.

{
    public class MentalHealthReport
    {
        // Primary key
        public int Id { get; set; }

        // Title of the report
        public string Title { get; set; } = string.Empty;

        // Date the report was generated
        public DateTime GeneratedDate { get; set; }

        // Type of report e.g. Monthly, Weekly
        public string ReportType { get; set; } = string.Empty;

        // Time period the report covers
        public string TimePeriod { get; set; } = string.Empty;

        // Snapshot of anonymized data included in the report
        public string DataSnapshot { get; set; } = string.Empty;

        // Admin who generated the report
        public string GeneratedBy { get; set; } = string.Empty;
    }
}