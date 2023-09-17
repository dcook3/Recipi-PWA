using Recipi_PWA.Models;
using System.Net.Http.Json;

namespace Recipi_PWA.Services
{
    public class MessageService : DefaultHttpService, IMessageService
    {
        public MessageService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state)
        {
        }

        public async Task<HttpResponseMessage> GetConversations() => await client.GetAsync($"/api/UserMessaging/conversations");
        public async Task<HttpResponseMessage> CreateConversation(int userId) => await client.PostAsync($"/api/UserMessaging/conversations?userId={userId}", null);
    }
}
