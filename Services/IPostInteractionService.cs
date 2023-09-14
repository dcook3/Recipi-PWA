namespace Recipi_PWA.Services
{
    public interface IPostInteractionService
    {
        Task<HttpResponseMessage> AddToCookbook(int recipeId);
        Task<HttpResponseMessage> CommentPost(int postId, string comment);
        Task<HttpResponseMessage> GetComments(int postId);
        Task<HttpResponseMessage> LikePost(int postId);
        Task<HttpResponseMessage> ReportPost(int postId, string message);
    }
}