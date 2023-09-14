using Recipi_PWA.Models.PostView;

namespace Recipi_PWA.Models
{
    public partial class PostReport
    {
        public int ReportId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = null!;
        public string? Status { get; set; }
        public DateTime ReportedDatetime { get; set; }
    }
}
