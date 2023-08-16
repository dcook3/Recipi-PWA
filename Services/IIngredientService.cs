namespace Recipi_PWA.Services
{
    public interface IIngredientService
    {
        Task<HttpResponseMessage> GetIngredients();
    }
}