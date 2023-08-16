
namespace Recipi_PWA.Models
{
    public class Ingredient
    {
        public int ingredientId { get; set; }
        public string ingredientTitle { get; set; }
        public string ingredientDescription { get; set; }
        public int createdByUserId { get; set; }
        public string ingredientIcon { get; set; }
        public object createdByUser { get; set; }
        public StepIngredient[] stepIngredients { get; set; }
    }
}
