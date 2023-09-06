using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public interface IHelperService
    {
        string FormatDescription(string desc, List<StepIngredient> ings);
    }
}