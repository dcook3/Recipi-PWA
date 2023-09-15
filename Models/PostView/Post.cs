namespace Recipi_PWA.Models.PostView
{
    public class Post
    {
        public int PostId { get; set; }

        public string PostTitle { get; set; }

        public string PostDescription { get; set; }

        public string? PostMedia { get; set; }

        public string ThumbnailUrl { get; set; }

        public PostUser User { get; set; }

        public PostRecipe? Recipe { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public bool HasLiked { get; set; }
    }
}
