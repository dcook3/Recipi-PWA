using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public interface IPostService : IDefaultHttpService
    {
        Task<HttpResponseMessage> GetUserPosts(int userId);
        Task<HttpResponseMessage> CreatePost(PostData postData);
        Task<HttpResponseMessage> GetPost(int postId);

        Task<HttpResponseMessage> GetReccomendedPosts();
        Task<HttpResponseMessage> GetFollowingPosts();
    }
}