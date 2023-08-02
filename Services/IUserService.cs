using Recipi_API.Models;

namespace Recipi_PWA
{
    public interface IUserService : IDefaultHttpService
    {
        Task<HttpResponseMessage> Login(UserLogin login);
        Task<HttpResponseMessage> GetUserById(string userId);
    }
}