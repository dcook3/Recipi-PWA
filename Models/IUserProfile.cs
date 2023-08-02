namespace Recipi_PWA.Models
{
    public interface IUserProfile
    {
        string? Biography { get; set; }
        string? ProfilePicture { get; set; }
        UserStats userStats { get; set; }
        int UserId { get; set; }
        string Username { get; set; }
    }
}