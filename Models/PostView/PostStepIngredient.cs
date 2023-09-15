using Recipi_PWA.Models;

namespace Recipi_PWA.Models.PostView
{
    public class PostStepIngredient : IStepIngredient
    {
        public int StepIngredientId { get; set; }
        public string IngredientMeasurementUnit { get; set; }
        public float IngredientMeasurementValue { get; set; }
        
        public Ingredient Ingredient { get; set; }
}
}
