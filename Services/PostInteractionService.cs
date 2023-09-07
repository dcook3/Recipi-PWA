using Recipi_PWA.Models;
using static System.Web.HttpUtility;

namespace Recipi_PWA.Services
{
    public class PostInteractionService : DefaultHttpService, IPostInteractionService
    {
        public PostInteractionService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state) { }

        //LikePost
        public async Task<HttpResponseMessage> LikePost(int postId)
        {
            return await client.PostAsync($"/api/Posts/{postId}/like", null);
        }
        //CommentPost
        public async Task<HttpResponseMessage> CommentPost(int postId, string comment)
        {
            return await client.PostAsync($"/api/Posts/{postId}/comments?comment={UrlEncode(comment)}", null);
        }
        //GetComments
        public async Task<HttpResponseMessage> GetComments(int postId)
        {
            return await client.GetAsync($"/api/Posts/{postId}/comments");
        }
        //AddToCookbook
        public async Task<HttpResponseMessage> AddToCookbook(int recipeId)
        {
            return await client.PutAsync($"/api/Recipes/{recipeId}/cookbook", null);
        }
        //ReportPost
        public async Task<HttpResponseMessage> ReportPost(int postId, string message)
        {
            return await client.PostAsync($"/api/Posts/{postId}/report?message={UrlEncode(message)}", null);
        }
        //ReportBug ?
    }
}
