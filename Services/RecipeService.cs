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

        public async Task<HttpResponseMessage> PostRecipe(Recipe recipe) => await client.PostAsJsonAsync($"/api/Recipes", recipe);

        public async Task<HttpResponseMessage> PutRecipe(int recipeId, Recipe recipe) => await client.PutAsJsonAsync($"/api/Recipes/{recipeId}", recipe);

        public async Task<HttpResponseMessage> DeleteRecipe(int recipeId) => await client.DeleteAsync($"/api/Recipes/{recipeId}");

        //To avoid the compiler not knowing which overloaded PutAsJson method when supplied null, i supplied it with a recipe so I can build. I dont think this results in any issues -Dylan
        public async Task<HttpResponseMessage> AddToCookbook(int recipeId) => await client.PutAsJsonAsync($"/api/Recipes/{recipeId}/cookbook", new Recipe());
    }
}
