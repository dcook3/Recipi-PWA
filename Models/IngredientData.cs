namespace Recipi_PWA.Models
{
    public class IngredientData
    {
        public IngredientData(string ingredientTitle, string ingredientDescription, string ingredientIcon, int userId)
        {
            this.IngredientTitle = ingredientTitle;
            this.IngredientDescription = ingredientDescription;
            this.IngredientIcon = ingredientIcon;
            CreatedByUserId = userId;
        }

        public string IngredientTitle { get; set; }
        public string? IngredientDescription { get; set; }
        public string? IngredientIcon { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
