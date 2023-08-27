using Recipi_PWA.Models;
using System.Net.Http.Json;

namespace Recipi_PWA.Services
{
    public class IngredientService : DefaultHttpService, IIngredientService
    {
        public IngredientService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state) { }

        public async Task<HttpResponseMessage> GetIngredients() => await client.GetAsync("/api/Ingredients");
        public async Task<HttpResponseMessage> SearchIngredients(string keyword) => await client.GetAsync($"/api/Ingredients/search/{keyword}");
        public async Task<HttpResponseMessage> CreateIngredient(IngredientData ingredient) => await client.PostAsJsonAsync($"/api/Ingredients", ingredient);
    }
}
