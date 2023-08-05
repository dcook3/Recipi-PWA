using Recipi_API.Models;
using Recipi_PWA.Models;

namespace Recipi_PWA
{
    public abstract class DefaultHttpService : IDefaultHttpService
    {
        protected readonly HttpClient client;
        protected readonly StateContainer state;
        private bool tokenSet;
        public DefaultHttpService(HttpClient httpClient, StateContainer _state)
        {
            this.state = _state;
            this.client = httpClient;
            if (state.LoggedIn)
            {
                SetToken();
            }
            state.OnChange += SetToken;
        }
        public void SetToken()
        {
            if (client.DefaultRequestHeaders.Any(h => h.Key == "Authorization"))
                client.DefaultRequestHeaders.Remove("Authorization");

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + state.Token);
            tokenSet = true;
        }
        public void Logout()
        {
            if (client.DefaultRequestHeaders.Any(h => h.Key == "Authorization"))
                client.DefaultRequestHeaders.Remove("Authorization");
        }
    }
}