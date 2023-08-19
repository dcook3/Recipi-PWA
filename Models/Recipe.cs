using System.Text.Json.Serialization;

namespace Recipi_PWA.Models
{
    public class Recipe
    {
        public Recipe()
        {
            recipeTitle = "Change title here";
            recipeDescription = "Add a description...";
            recipeSteps = new();
        }

        public Recipe(int recipeId)
        {
            this.recipeId = recipeId;
            recipeTitle = "Change title here";
            recipeDescription = "Add a description...";
            recipeSteps = new();
        }

        [JsonIgnore]
        public int recipeId { get; set; }

        public string recipeTitle  { get; set; }

        public string? recipeDescription { get; set; }

        public List<RecipeStep> recipeSteps { get; set; }

        private static void Swap<T>(List<T> list, int first, int second)
        {
            T temp = list[first];
            list[first] = list[second];
            list[second] = temp;
        }

        public void OrganizeSteps()
        {
            for (int i = 1; i < recipeSteps.Count; i++)
            {
                int j = i;
                while (j > 0 && recipeSteps[j].stepOrder.CompareTo(recipeSteps[j - 1].stepOrder) < 0)
                {
                    Swap(recipeSteps, j, j - 1);
                    j--;
                }
            }
        }


    }
}
