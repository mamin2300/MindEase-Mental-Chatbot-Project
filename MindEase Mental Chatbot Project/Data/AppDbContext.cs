
// AppDbContext.cs
// Author: Shayma
// Database context for MindEase - registers all entity tables for EF Core.

using Microsoft.EntityFrameworkCore;
using MindEase_Mental_Chatbot_Project.Models;

namespace MindEase_Mental_Chatbot_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Malika's tables
        public DbSet<MentalHealthReport> MentalHealthReports { get; set; }
        public DbSet<UsageMetric> UsageMetrics { get; set; }

        // Azm's tables
        public DbSet<MoodEntry> MoodEntries { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        // Mamin's tables
        public DbSet<TriageResult> TriageResults { get; set; }

        // Shayma's tables
        public DbSet<ApplicationUser> Users { get; set; }
    }
}