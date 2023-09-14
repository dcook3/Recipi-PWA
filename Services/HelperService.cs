using Recipi_PWA.Models;
using Recipi_PWA.Models.PostView;

namespace Recipi_PWA.Services
{
    public class HelperService : IHelperService
    {
        public HelperService()
        {
        }

        public string FormatDescription(string desc, List<StepIngredient> ingredients)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                string ingFormatted;
                if (ingredients[i].IngredientMeasurementValue == 1)
                {
                    ingFormatted = $"{ingredients[i].IngredientMeasurementValue} {ingredients[i].IngredientMeasurementUnit} {ingredients[i].Ingredient.ingredientTitle}";
                }
                else
                {
                    ingFormatted = $"{ingredients[i].IngredientMeasurementValue} {ingredients[i].IngredientMeasurementUnit}s {ingredients[i].Ingredient.ingredientTitle}";
                }
                string checkString = "{" + i.ToString() + "}";
                desc = desc.Replace(checkString, ingFormatted);
            }
            return desc;
        }
        public string FormatDescription(string desc, List<PostStepIngredient> ingredients)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                string ingFormatted;
                if (ingredients[i].IngredientMeasurementValue == 1)
                {
                    ingFormatted = $"{ingredients[i].IngredientMeasurementValue} {ingredients[i].IngredientMeasurementUnit} {ingredients[i].Ingredient.ingredientTitle}";
                }
                else
                {
                    ingFormatted = $"{ingredients[i].IngredientMeasurementValue} {ingredients[i].IngredientMeasurementUnit}s {ingredients[i].Ingredient.ingredientTitle}";
                }
                string checkString = "{" + i.ToString() + "}";
                desc = desc.Replace(checkString, ingFormatted);
            }
            return desc;
        }
    }
}
