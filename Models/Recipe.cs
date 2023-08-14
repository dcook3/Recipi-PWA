using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Http;

namespace Recipi_PWA.Models
{
    public class Recipe
    {
        private int recipeId;

        private string recipeTitle = null!;

        private string? recipeDescription;

        private int userId;

        private DateTime createdDatetime;

        private ICollection<RecipeStep> recipeSteps = new List<RecipeStep>();

        public int RecipeId { get => recipeId; set { recipeId = value; RaisePropertyChanged(); } }
        public string RecipeTitle { get => recipeTitle; set { recipeTitle = value; RaisePropertyChanged(); } }
        public string? RecipeDescription { get => recipeDescription; set { recipeDescription = value; RaisePropertyChanged(); } }
        public int UserId { get => userId; set { userId = value; RaisePropertyChanged(); } }
        public DateTime CreatedDatetime { get => createdDatetime; set { createdDatetime = value; RaisePropertyChanged(); } }
        public ICollection<RecipeStep> RecipeSteps { get => recipeSteps; set { recipeSteps = value; RaisePropertyChanged(); } }

        public bool IsStateless { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(!IsStateless)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
