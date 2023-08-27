using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public interface IIngredientService
    {
        Task<HttpResponseMessage> GetIngredients();
        Task<HttpResponseMessage> SearchIngredients(string keyword);
        Task<HttpResponseMessage> CreateIngredient(IngredientData ingredient);
    }
}