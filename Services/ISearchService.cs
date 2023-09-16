namespace Recipi_PWA.Services
{
    public interface ISearchService : IDefaultHttpService
    {
        Task<HttpResponseMessage> Search(string query);
    }
}