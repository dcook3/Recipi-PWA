using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public class IngredientService : DefaultHttpService, IIngredientService
    {
        public IngredientService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state) { }

        public async Task<HttpResponseMessage> GetIngredients() => await client.GetAsync("/api/Ingredients");
    }
}
