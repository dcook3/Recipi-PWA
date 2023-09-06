using Recipi_PWA.Models;
using Recipi_PWA.Models.PostView;

namespace Recipi_PWA.Services
{
    public class HelperService<T> : IHelperService<T> where T : class
    {
        public string FormatDescription(string desc, List<T> ings)
        {
            for (int i = 0; i < ings.Count; i++)
            {
                if (typeof(T) == typeof(StepIngredient))
                {
                    List<StepIngredient> ingredients = ings as List<StepIngredient> ?? throw new ArrayTypeMismatchException();
                    string ingFormatted;
                    if (ingredients[i].ingredientMeasurementValue == 1)
                    {
                        ingFormatted = $"{ingredients[i].ingredientMeasurementValue} {ingredients[i].ingredientMeasurementUnit} {ingredients[i].ingredient.ingredientTitle}";
                    }
                    else
                    {
                        ingFormatted = $"{ingredients[i].ingredientMeasurementValue} {ingredients[i].ingredientMeasurementUnit}s {ingredients[i].ingredient.ingredientTitle}";
                    }
                    string checkString = "{" + i.ToString() + "}";
                    desc = desc.Replace(checkString, ingFormatted);
                }
                else
                {
                    List<PostStepIngredient> ingredients = ings as List<PostStepIngredient> ?? throw new ArrayTypeMismatchException();
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
                
            }
            return desc;
        }
    }
}
