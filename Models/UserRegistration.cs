
using System.ComponentModel.DataAnnotations;

namespace Recipi_PWA.Models
{
    public class UserRegistration
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(254)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [MaxLength(2048)]
        public string? ProfilePicture { get; set; }

        [MaxLength(200)]
        public string? Biography { get; set; }
    }
}
