using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public interface IRecipeService : IDefaultHttpService
    {
        Task<HttpResponseMessage> GetCookbook(string sortBy);
        Task<HttpResponseMessage> GetCookbook(int userId, string sortBy);
        Task<HttpResponseMessage> GetRecipeById(int recipeId);

        Task<HttpResponseMessage> RemoveRecipeFromCookbook(int recipeId);
        Task<HttpResponseMessage> PostRecipe(Recipe recipe);
        Task<HttpResponseMessage> PutRecipe(int recipeId, RecipeUpdate recipe);
        Task<HttpResponseMessage> DeleteRecipe(int recipeId);
        Task<HttpResponseMessage> DissociateRecipe(int recipeId);
        Task<HttpResponseMessage> AddToCookbook(int recipeId);
    }
}