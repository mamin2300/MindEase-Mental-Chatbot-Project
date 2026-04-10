
    using Microsoft.EntityFrameworkCore;
    using MindEase_Mental_Chatbot_Project.Data;
    using MindEase_Mental_Chatbot_Project.Models;
    namespace MindEase_Mental_Chatbot_Project.Services
    {

    // Author: Malika
    // Implements IAnalyticsService - handles all analytics and report operations.
    public class AnalyticsService : IAnalyticsService
        {
            private readonly AppDbContext _context;

            // Constructor - AppDbContext injected via dependency injection
            public AnalyticsService(AppDbContext context)
            {
                _context = context;
            }

            // Get all reports from the database
            public async Task<List<MentalHealthReport>> GetAllReportsAsync()
            {
                return await _context.MentalHealthReports.ToListAsync();
            }

            // Get a single report by its id
            public async Task<MentalHealthReport?> GetReportByIdAsync(int id)
            {
                return await _context.MentalHealthReports.FindAsync(id);
            }

            // Save a new report to the database
            public async Task AddReportAsync(MentalHealthReport report)
            {
                report.GeneratedDate = DateTime.Now;
                _context.MentalHealthReports.Add(report);
                await _context.SaveChangesAsync();
            }

            // Get all usage metrics
            public async Task<List<UsageMetric>> GetAllMetricsAsync()
            {
                return await _context.UsageMetrics.ToListAsync();
            }

            // Build a summary dictionary for the dashboard
            public async Task<Dictionary<string, double>> GetDashboardSummaryAsync()
            {
                var metrics = await _context.UsageMetrics.ToListAsync();

                return new Dictionary<string, double>
            {
                { "TotalReports", await _context.MentalHealthReports.CountAsync() },
                { "ActiveUsers", metrics.Where(m => m.MetricType == "ActiveUsers").Sum(m => m.Value) },
                { "MoodEntries", metrics.Where(m => m.MetricType == "MoodEntries").Sum(m => m.Value) },
                { "ChatSessions", metrics.Where(m => m.MetricType == "ChatSessions").Sum(m => m.Value) }
            };
            }
        }
    }

