using Recipi_PWA;
using Recipi_PWA.Models;
using static System.Web.HttpUtility;
namespace Recipi_PWA.Services
{
    public class RecipeService : DefaultHttpService, IRecipeService
    {
        public RecipeService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state) { }


        public async Task<HttpResponseMessage> GetCookbook(string sortBy) => await client.GetAsync($"/api/Recipes/cookbook?sortBy={UrlEncode(sortBy)}");
        public async Task<HttpResponseMessage> GetCookbook(int userId, string sortBy) => await client.GetAsync($"/api/Recipes/cookbook/User/{userId}?sortBy={UrlEncode(sortBy)}");

        public async Task<HttpResponseMessage> GetRecipe(int recipeId) => await client.GetAsync($"/api/Recipes/{recipeId}");

        public async Task<HttpResponseMessage> SetRecipe(Recipe recipe) { await client.PostAsync($"/api/Recipes", { })};
    }
}
