using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public interface IMessageService : IDefaultHttpService
    {
        Task<HttpResponseMessage> GetConversations();
        Task<HttpResponseMessage> GetConversationByUserId(int userId);
        Task<HttpResponseMessage> CreateConversation(int userId);
        Task<HttpResponseMessage> GetConversationMessages(int conversationId);
        Task<HttpResponseMessage> AddMessageToConversation(int conversationId, string msgContents);
    }
}