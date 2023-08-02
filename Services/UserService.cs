using Recipi_API.Models;
using System.Net.Http.Json;

namespace Recipi_PWA
{
    public class UserService : DefaultHttpService, IUserService
    {
        public UserService(HttpClient httpClient) : base(httpClient)
        {
        }

        // private readonly HttpClient client;
        // public UserService(HttpClient httpClient)
        // {
        //     this.client = httpClient;
        // }

        public async Task<HttpResponseMessage> Login(UserLogin login) => await client.PostAsJsonAsync("/api/Users/Login", login);
        
        public async Task<HttpResponseMessage> GetUserById(string userId) => await client.GetAsync($"/api/Users/{userId}");
    }
}
