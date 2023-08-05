using Recipi_API.Models;
using Recipi_PWA.Models;
using System.Net.Http.Json;

namespace Recipi_PWA
{
    public class UserService : DefaultHttpService, IUserService
    {
        public UserService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state)
        {
        }

        // private readonly HttpClient client;
        // public UserService(HttpClient httpClient)
        // {
        //     this.client = httpClient;
        // }

        public async Task<HttpResponseMessage> Login(UserLogin login) => await client.PostAsJsonAsync("/api/Users/Login", login);
        
        public async Task<HttpResponseMessage> GetUserById(string userId) => await client.GetAsync($"/api/Users/{userId}");

        public async Task<HttpResponseMessage> GetFriends() => await client.GetAsync("/api/Users/Friend");
        public async Task<HttpResponseMessage> GetFriendRequests() => await client.GetAsync("/api/Users/Friend/Requests");

        public async Task<HttpResponseMessage> GetRelationship(int userId, string relationshipType) => await client.GetAsync($"/api/Users/{userId}/{relationshipType}");

        public async Task<HttpResponseMessage> FollowUser(int userId) => await client.PostAsync($"/api/Users/Follow/{userId}", null);

        public async Task<HttpResponseMessage> SendFriendRequest(int userId) => await client.PostAsync($"/api/Users/Friend/Request/{userId}", null);
        public async Task<HttpResponseMessage> AcceptFriendRequest(int userId) => await client.PostAsync($"/api/Users/Friend/Accept/{userId}", null);
        public async Task<HttpResponseMessage> DenyFriendRequest(int userId) => await client.DeleteAsync($"/api/Users/Friend/Deny/{userId}");
        public async Task<HttpResponseMessage> RemoveFriend(int userId) => await client.DeleteAsync($"/api/Users/Friend/Remove/{userId}");
    }
}
