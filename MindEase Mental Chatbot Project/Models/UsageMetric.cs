namespace MindEase_Mental_Chatbot_Project.Models
{
    
    // Author: Malika
    // Represents a usage metric data point tracked by the analytics service.

        public class UsageMetric
        {
            // Primary key
            public int Id { get; set; }

            // Type of metric e.g. ActiveUsers, MoodEntries, ChatSessions
            public string MetricType { get; set; } = string.Empty;

            // Numeric value of the metric
            public double Value { get; set; }

            // When this metric was recorded
            public DateTime Timestamp { get; set; }

            // Time period this metric covers e.g. Daily, Weekly, Monthly
            public string AggregationPeriod { get; set; } = string.Empty;
        }
    }

