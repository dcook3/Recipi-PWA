using System.ComponentModel.DataAnnotations;

namespace Recipi_PWA.Models
{
    public class ProfileUpdate
{
    [Required]
    [MaxLength(30)]
    public string Username { get; set; } = null!;


    [MaxLength(2048)]
    public string? ProfilePicture { get; set; }

        [MaxLength(200)]
        public string? Biography { get; set; }
}
}
