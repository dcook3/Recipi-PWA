using Recipi_PWA;
using Recipi_PWA.Models;
using System.Net.Http.Json;
using static System.Web.HttpUtility;
namespace Recipi_PWA.Services
{
    public class RecipeService : DefaultHttpService, IRecipeService
    {
        public RecipeService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state) { }

        public async Task<HttpResponseMessage> GetCookbook(string sortBy) => await client.GetAsync($"/api/Recipes/cookbook?sortBy={UrlEncode(sortBy)}");
        public async Task<HttpResponseMessage> GetCookbook(int userId, string sortBy) => await client.GetAsync($"/api/Recipes/cookbook/User/{userId}?sortBy={UrlEncode(sortBy)}");

        public async Task<HttpResponseMessage> GetRecipeById(int recipeId) => await client.GetAsync($"/api/Recipes/{recipeId}");

        public async Task<HttpResponseMessage> PostRecipe(Recipe recipe) => await client.PostAsync($"/api/Recipes", JsonContent.Create(recipe));
    }
}
