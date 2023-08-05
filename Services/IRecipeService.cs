namespace Recipi_PWA.Services
{
    public interface IRecipeService : IDefaultHttpService
    {
        Task<HttpResponseMessage> GetCookbook(string sortBy);
        Task<HttpResponseMessage> GetCookbook(int userId, string sortBy);
    }
}