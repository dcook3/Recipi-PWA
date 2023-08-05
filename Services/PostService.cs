using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public class PostService : DefaultHttpService, IPostService
    {
        public PostService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state)
        {
        }

        public async Task<HttpResponseMessage> GetUserPosts(int userId) => await client.GetAsync($"/api/Posts/user/{userId}");
    }
}
