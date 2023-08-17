
namespace Recipi_PWA.Models
{
    public class Ingredient
    {
        public int ingredientId { get; set; }
        public string ingredientTitle { get; set; }
        public string ingredientDescription { get; set; }
        public string ingredientIcon { get; set; }
        public List<Ingredient> stepIngredients { get; set; }
    }
}
