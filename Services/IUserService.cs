using Recipi_API.Models;
using Recipi_PWA.Models;

namespace Recipi_PWA
{
    public interface IUserService : IDefaultHttpService
    {
        Task<HttpResponseMessage> Login(UserLogin login);
        Task<HttpResponseMessage> Register(UserRegistration registration);
        Task<HttpResponseMessage> GetUserById(string userId);
        Task<HttpResponseMessage> GetFriends();
        Task<HttpResponseMessage> GetFriendRequests();
        Task<HttpResponseMessage> GetRelationship(int userId, string relationshipType);


        Task<HttpResponseMessage> FollowUser(int userId);
        Task<HttpResponseMessage> SendFriendRequest(int userId);
        Task<HttpResponseMessage> AcceptFriendRequest(int userId);
        Task<HttpResponseMessage> DenyFriendRequest(int userId);
        Task<HttpResponseMessage> RemoveFriend(int userId);
        Task<HttpResponseMessage> ChangeProfilePicture(string profilePicture);
    }
}