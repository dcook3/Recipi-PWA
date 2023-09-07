using Recipi_PWA.Models;
using Recipi_PWA.Models.PostView;

namespace Recipi_PWA.Services
{
    public interface IHelperService
    {
        string FormatDescription(string desc, List<StepIngredient> ings);
        string FormatDescription(string desc, List<PostStepIngredient> ings);
    }
}