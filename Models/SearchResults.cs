using Recipi_PWA.Models.PostView;

namespace Recipi_PWA.Models
{
    public class SearchResults
    {
        public List<PostUser> Users { get; set; } = new();

        public List<SearchPostResult> Posts { get; set; } = new();

        public List<SearchRecipeResult> Recipes { get; set; } = new();
    }
}
