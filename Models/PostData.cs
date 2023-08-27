namespace Recipi_PWA.Models
{
    public class PostData
    {
        public string? PostTitle { get; set; } = null!;

        public string? PostDescription { get; set; } = null!;

        public int? RecipeId { get; set; } = -1!;

        public string? PostMedia { get; set; }

        public string? ThumbnailUrl { get; set; } = null!;

        public List<PostMediaData> PostMediaList { get; set; } = new()!;
    }
}
