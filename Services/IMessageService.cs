using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public interface IMessageService : IDefaultHttpService
    {
        Task<HttpResponseMessage> GetConversations();
        Task<HttpResponseMessage> CreateConversation(int userId);
    }
}