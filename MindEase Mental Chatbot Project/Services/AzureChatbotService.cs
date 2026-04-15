using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

// Alias to avoid name clash with our ChatMessage model
using AzureSystemMessage = OpenAI.Chat.SystemChatMessage;
using AzureUserMessage = OpenAI.Chat.UserChatMessage;

namespace MindEase_Mental_Chatbot_Project.Services
{
    public class AzureChatbotService : IChatbotService
    {
        private readonly ChatClient? _chatClient;
        private readonly ChatbotService _fallback = new();

        private const string SystemPrompt =
            "You are MindEase, a compassionate mental health support chatbot for college students. " +
            "Your role is to listen actively, validate feelings, and offer evidence-based coping strategies. " +
            "Keep responses warm, concise (2-4 sentences), and conversational. " +
            "Never diagnose or replace a licensed professional. " +
            "If a student mentions self-harm or suicide, always provide these crisis resources immediately: " +
            "Crisis Services Canada 1-833-456-4566 (24/7), Text HOME to 741741, or call 911.";

        public AzureChatbotService(IConfiguration config)
        {
            var endpoint = config["AzureOpenAI:Endpoint"];
            var apiKey = config["AzureOpenAI:ApiKey"];
            var deployment = config["AzureOpenAI:DeploymentName"];

            if (!string.IsNullOrWhiteSpace(endpoint)
                && !string.IsNullOrWhiteSpace(apiKey)
                && !string.IsNullOrWhiteSpace(deployment)
                && !endpoint.Contains("YOUR-RESOURCE-NAME"))
            {
                var client = new AzureOpenAIClient(
                    new Uri(endpoint),
                    new AzureKeyCredential(apiKey));
                _chatClient = client.GetChatClient(deployment);
            }
        }

        public async Task<string> GetResponseAsync(string message)
        {
            if (_chatClient == null)
                return _fallback.GetResponse(message);

            try
            {
                var messages = new List<OpenAI.Chat.ChatMessage>
                {
                    new AzureSystemMessage(SystemPrompt),
                    new AzureUserMessage(message)
                };

                ChatCompletion result = await _chatClient.CompleteChatAsync(messages);
                return result.Content[0].Text;
            }
            catch
            {
                // Falls back to keyword responses if Azure call fails
                return _fallback.GetResponse(message);
            }
        }

        // Sync fallback
        public string GetResponse(string message) => _fallback.GetResponse(message);
    }
}