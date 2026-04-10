// IAnalyticsService.cs
// Author: Malika
// Interface for the analytics service - defines the public contract for analytics operations.

using MindEase_Mental_Chatbot_Project.Models;

namespace MindEase_Mental_Chatbot_Project.Services
{
    public interface IAnalyticsService
    {
        // Get all generated reports
        Task<List<MentalHealthReport>> GetAllReportsAsync();

        // Get a single report by id
        Task<MentalHealthReport?> GetReportByIdAsync(int id);

        // Save a new report to the database
        Task AddReportAsync(MentalHealthReport report);

        // Get all usage metrics
        Task<List<UsageMetric>> GetAllMetricsAsync();

        // Get summary metrics for the dashboard
        Task<Dictionary<string, double>> GetDashboardSummaryAsync();
    }
}