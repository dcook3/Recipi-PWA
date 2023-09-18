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
        public async Task<HttpResponseMessage> GetConversationByUserId(int userId) => await client.GetAsync($"/api/UserMessaging/conversations/{userId}");
        public async Task<HttpResponseMessage> CreateConversation(int userId) => await client.PostAsync($"/api/UserMessaging/conversations?userId={userId}", null);
        public async Task<HttpResponseMessage> GetConversationMessages(int conversationId) => await client.GetAsync($"/api/UserMessaging/messages/{conversationId}");
        public async Task<HttpResponseMessage> AddMessageToConversation(int conversationId, string msgContents) => await client.PostAsync($"/api/UserMessaging/messages?conversationId={conversationId}&messageContents={msgContents}", null);
    }
}
