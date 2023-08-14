namespace Recipi_PWA.Models
{
    public class RecipeStep
    {
        public int stepId { get; set; }
        public string stepDescription { get; set; }
        public int stepOrder { get; set; }
        public StepIngredient[] stepIngredients { get; set; }
    }
}