using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public class SearchService : DefaultHttpService, ISearchService
    {
        public SearchService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state)
        {
        }

        public async Task<HttpResponseMessage> Search(string query) => await client.GetAsync($"/api/Search/{query}");
    }
}
