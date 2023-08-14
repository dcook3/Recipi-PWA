using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Http;

namespace Recipi_PWA.Models
{
    public class Recipe
    {
        private int _recipeId;

        private string _recipeTitle = null!;

        private string? _recipeDescription;

        private string _createdByUsername;

        private DateTime _createdDatetime;

        private RecipeStep[] _recipeSteps;

        public int recipeId { get => _recipeId; set { _recipeId = value; RaisePropertyChanged(); } }
        public string recipeTitle { get => _recipeTitle; set { _recipeTitle = value; RaisePropertyChanged(); } }
        public string? recipeDescription { get => _recipeDescription; set { _recipeDescription = value; RaisePropertyChanged(); } }
        public string createdByUsername { get => _createdByUsername; set { _createdByUsername = value; RaisePropertyChanged(); } }
        public DateTime createdDatetime { get => _createdDatetime; set { _createdDatetime = value; RaisePropertyChanged(); } }
        public RecipeStep[] recipeSteps { get => _recipeSteps; set { _recipeSteps = value; RaisePropertyChanged(); } }

        public bool IsStateless { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(!IsStateless)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
