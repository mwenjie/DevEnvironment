using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace BlazorApp.Data
{
    interface IChatBotService
    {
        Task<string> Chat(string en_text);
    }
    public class ChatBotService : IChatBotService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ChatBotService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Chat(string en_text)
        {
            var httpClient = _httpClientFactory.CreateClient();
            using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                en_text = en_text
            }),
            Encoding.UTF8,
            "application/json");

            var response = await httpClient.PostAsync("http://host.docker.internal:8000/chat", jsonContent);
            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
            var outputText = result["text"];
            if (outputText != null)
            {
                return outputText.ToString();
            }
            else
            {
                return "Error";
            }
        }
    }
}