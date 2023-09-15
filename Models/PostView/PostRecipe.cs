namespace Recipi_PWA.Models.PostView
{
    public class PostRecipe
    {
        public int RecipeId { get; set; }

        public string RecipeTitle { get; set; }

        public string RecipeDescription { get; set; }

        public bool HasAddedToCookbook { get; set; }

        public PostUser User { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public List<PostRecipeStep> RecipeSteps { get; set; } = new();
    }
}
