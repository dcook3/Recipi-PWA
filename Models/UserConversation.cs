namespace Recipi_PWA.Models
{
    public class UserConversation
    {
        public int ConversationId { get; set; }

        public int UserId1 { get; set; }

        public int UserId2 { get; set; }

        public virtual ICollection<UserMessage>? Messages { get; set; } = new List<UserMessage>();
    }
}
