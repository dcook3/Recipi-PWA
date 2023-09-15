using System.Text.Json.Serialization;

namespace Recipi_PWA.Models
{
    public class StepIngredient : IStepIngredient
    {
        public StepIngredient()
        {
            ingredientMeasurementValue = 0;
            ingredientMeasurementUnit = "";
            ingredient = new();
        }
        

        public int stepIngredientId { get; set; }
        public string ingredientMeasurementUnit { get; set; }
        public float ingredientMeasurementValue { get; set; }
        public Ingredient ingredient { get; set; }
    }
}
