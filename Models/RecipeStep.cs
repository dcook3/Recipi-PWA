namespace Recipi_PWA.Models
{
    public class RecipeStep
    {
        public int StepId { get; set; }

        public string StepDescription { get; set; } = null!;

        public int RecipeId { get; set; }

        public short StepOrder { get; set; }
    }
}