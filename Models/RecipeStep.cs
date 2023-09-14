using System.Text.Json.Serialization;

namespace Recipi_PWA.Models
{
    public class RecipeStep
    {
        public RecipeStep()
        {
            stepOrder = 0;
            stepDescription = "Step Example";
            stepIngredients = new();
        }

        public RecipeStep(string stepDescription, int stepOrder)
        {
            this.stepDescription = stepDescription;
            this.stepOrder = stepOrder;
            this.stepIngredients = new();
        }

        
        public int stepId { get; set; }
        public string stepDescription { get; set; }
        public int stepOrder { get; set; }
        public List<StepIngredient> stepIngredients { get; set; }
    }
}