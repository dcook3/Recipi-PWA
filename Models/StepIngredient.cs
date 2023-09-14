
using System.Text.Json.Serialization;

namespace Recipi_PWA.Models
{
    public class StepIngredient : IStepIngredient
    {
        public StepIngredient()
        {
            IngredientMeasurementValue = 0;
            IngredientMeasurementUnit = "";
            Ingredient = new();
        }
        

        public int IngredientId { get; set; }
        public string IngredientMeasurementUnit { get; set; }
        public float IngredientMeasurementValue { get; set; }
        [JsonIgnore]
        public Ingredient Ingredient { get; set; }
    }
}
