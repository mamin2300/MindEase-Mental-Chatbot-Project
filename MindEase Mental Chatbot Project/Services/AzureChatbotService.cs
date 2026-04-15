using System.Text;
using System.Text.Json;

namespace MindEase_Mental_Chatbot_Project.Services
{
    public class AzureChatbotService : IChatbotService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string? _apiKey;
        private readonly ChatbotService _fallback = new();

        private const string SystemPrompt =
            "You are MindEase, a compassionate mental wellness chatbot for university students. " +
            "Respond with empathy, keep answers concise (2-4 sentences), and always encourage " +
            "professional help for serious concerns. Never diagnose. Be warm and supportive. " +
            "If a student mentions self-harm or suicide, always provide these crisis resources: " +
            "Crisis Services Canada 1-833-456-4566 (24/7), Text HOME to 741741, or call 911.";

        public AzureChatbotService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = config["Groq:ApiKey"];
        }

        public async Task<string> GetResponseAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
                return _fallback.GetResponse(message);

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var requestBody = new
                {
                    model = "llama-3.3-70b-versatile",
                    messages = new[]
                    {
                        new { role = "system", content = SystemPrompt },
                        new { role = "user", content = message }
                    },
                    max_tokens = 200
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return $"[Groq error {response.StatusCode}]: {responseJson}";

                using var doc = JsonDocument.Parse(responseJson);
                var text = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return text ?? _fallback.GetResponse(message);
            }
            catch (Exception ex)
            {
                return $"[Exception]: {ex.Message}";
            }
        }

        public string GetResponse(string message) => _fallback.GetResponse(message);
    }
}