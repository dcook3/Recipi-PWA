using System.ComponentModel.DataAnnotations;

namespace Recipi_PWA.Models
{
    public class RecipeStepUpdate
    {
        public string StepDescription { get; set; }
        public short StepOrder { get; set; }
        public virtual ICollection<StepIngredientUpdate> StepIngredients { get; set; } = new List<StepIngredientUpdate>();
    }
}