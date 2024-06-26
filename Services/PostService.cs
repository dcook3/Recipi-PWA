﻿using Recipi_PWA.Models;
using System.Net.Http.Json;

namespace Recipi_PWA.Services
{
    public class PostService : DefaultHttpService, IPostService
    {
        public PostService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state)
        {
        }

        public async Task<HttpResponseMessage> GetUserPosts(int userId) => await client.GetAsync($"/api/Posts/user/{userId}");

        public async Task<HttpResponseMessage> CreatePost(PostData postData) => await client.PostAsJsonAsync("/api/Posts", postData);

        public async Task<HttpResponseMessage> GetPost(int postId) => await client.GetAsync($"/api/Posts/{postId}");

        public async Task<HttpResponseMessage> GetReccomendedPosts() => await client.GetAsync("/api/Posts");
        public async Task<HttpResponseMessage> GetFollowingPosts() => await client.GetAsync("/api/Posts/following");
    }
}
