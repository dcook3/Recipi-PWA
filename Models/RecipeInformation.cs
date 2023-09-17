namespace Recipi_PWA.Models
{
    public class RecipeInformation
    {
        public RecipeInformation()
        {
            recipeId = -1;
            isNewRecipe = false;
        }

        public int recipeId { get; set; }

        public bool isNewRecipe { get; set; }

        public Recipe recipe { get; set; }

        public bool? justSelectedIngredient { get; set; } = false;
    }
}
