namespace Recipi_PWA.Models.PostView
{
    public class PostRecipeStep
    {
        public int StepId { get; set; }

        public string StepDescription { get; set; }

        public int StepOrder { get; set; }

        public PostStepMedia PostMedia { get; set; }

        public List<PostStepIngredient> StepIngredients { get; set; } = new();
    }
}
