namespace Recipi_PWA.Models
{
    public class CookbookRecipe
    {
        public int RecipeId { get; set; }

        public string RecipeTitle { get; set; }

        public string RecipeDescription { get; set; }

        public int UserId { get; set; }

        public string CreatedDatetime { get; set; }
    }
}
