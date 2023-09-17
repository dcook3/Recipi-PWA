using Recipi_PWA.Models;

namespace Recipi_PWA.Models
{
    public class UserMessage
    {
        public int MessageId { get; set; }

        public int SendingUserId { get; set; }

        public int ConversationId { get; set; }

        public string MessageContents { get; set; } = "";
    }
}
    