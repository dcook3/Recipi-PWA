namespace Recipi_PWA.Models
{
    [Serializable]
    public class PostComment
    {
        public int commentId { get; set; }
        public int postId { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string comment { get; set; }
        public DateTime commentDatetime { get; set; }
    }
}
