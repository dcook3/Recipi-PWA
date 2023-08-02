using Recipi_PWA.Models;

namespace Recipi_PWA.Models
{
    public class UserProfile : IUserProfile
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string? ProfilePicture { get; set; }

        public string? Biography { get; set; }

        public UserStats userStats { get; set; }
    }
}
