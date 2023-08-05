namespace Recipi_PWA.Services
{
    public interface IPostService : IDefaultHttpService
    {
        Task<HttpResponseMessage> GetUserPosts(int userId);
    }
}