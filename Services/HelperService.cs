using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public class HelperService : IHelperService
    {
        public string FormatDescription(string desc, List<StepIngredient> ings)
        {
            for (int i = 0; i < ings.Count; i++)
            {
                string ingFormatted;
                if (ings[i].ingredientMeasurementValue == 1)
                {
                    ingFormatted = $"{ings[i].ingredientMeasurementValue} {ings[i].ingredientMeasurementUnit} {ings[i].ingredient.ingredientTitle}";
                }
                else
                {
                    ingFormatted = $"{ings[i].ingredientMeasurementValue} {ings[i].ingredientMeasurementUnit}s {ings[i].ingredient.ingredientTitle}";
                }
                string checkString = "{" + i.ToString() + "}";
                desc = desc.Replace(checkString, ingFormatted);
            }
            return desc;
        }
    }
}
