using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public interface IRecipeService : IDefaultHttpService
    {
        Task<HttpResponseMessage> GetCookbook(string sortBy);
        Task<HttpResponseMessage> GetCookbook(int userId, string sortBy);
        Task<HttpResponseMessage> GetRecipeById(int recipeId);
        Task<HttpResponseMessage> PostRecipe(Recipe recipe);
        Task<HttpResponseMessage> PutRecipe(Recipe recipe);
    }
}