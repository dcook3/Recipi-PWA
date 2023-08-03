using Recipi_API.Models;

namespace Recipi_PWA
{
    public abstract class DefaultHttpService : IDefaultHttpService
    {
        protected readonly HttpClient client;
        private bool tokenSet;
        public DefaultHttpService(HttpClient httpClient)
        {
            this.client = httpClient;
            tokenSet = false;
        }
        public void SetToken(string token)
        {
            if (client.DefaultRequestHeaders.Any(h => h.Key == "Authorization"))
                client.DefaultRequestHeaders.Remove("Authorization");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            tokenSet = true;
        }
        public void Logout()
        {
            tokenSet = false;
            if (client.DefaultRequestHeaders.Any(h => h.Key == "Authorization"))
                client.DefaultRequestHeaders.Remove("Authorization");
        }
        public bool TokenSet()
        {
            return tokenSet;
        }
    }
}