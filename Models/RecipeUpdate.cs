namespace Recipi_PWA.Models
{
    public class RecipeUpdate
    {
        public RecipeUpdate(Recipe originalRecipe) 
        {
            RecipeTitle = originalRecipe.recipeTitle;
            RecipeDescription = originalRecipe.recipeDescription;
            foreach(RecipeStep step in originalRecipe.recipeSteps)
            {
                RecipeStepUpdate stepUpdate = new()
                {
                    StepDescription = step.stepDescription,
                    StepOrder = (short)step.stepOrder
                };
                foreach (StepIngredient stepIngredient in step.stepIngredients)
                {
                    stepUpdate.StepIngredients.Add(new()
                    {
                        IngredientId = stepIngredient.ingredient.ingredientId,
                        ingredientMeasurementUnit = stepIngredient.ingredientMeasurementUnit,
                        ingredientMeasurementValue = stepIngredient.ingredientMeasurementValue
                    });
                }
                RecipeSteps.Add(stepUpdate);
            }
        }
        public string? RecipeTitle { get; set; }

        public string? RecipeDescription { get; set; }

        public List<RecipeStepUpdate> RecipeSteps { get; set; } = new();
    }
}
