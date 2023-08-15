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

        private List<RecipeStep> _recipeSteps;

        public int recipeId { get => _recipeId; set { _recipeId = value; RaisePropertyChanged(); } }
        public string recipeTitle { get => _recipeTitle; set { _recipeTitle = value; RaisePropertyChanged(); } }
        public string? recipeDescription { get => _recipeDescription; set { _recipeDescription = value; RaisePropertyChanged(); } }
        public string createdByUsername { get => _createdByUsername; set { _createdByUsername = value; RaisePropertyChanged(); } }
        public DateTime createdDatetime { get => _createdDatetime; set { _createdDatetime = value; RaisePropertyChanged(); } }
        public List<RecipeStep> recipeSteps { 
            get => _recipeSteps; 
            set 
            { 
                _recipeSteps = value;
                RaisePropertyChanged();
            } 
        }

        private static void Swap<T>(List<T> list, int first, int second)
        {
            T temp = list[first];
            list[first] = list[second];
            list[second] = temp;
        }

        public void OrganizeSteps()
        {
            for (int i = 1; i < _recipeSteps.Count; i++)
            {
                int j = i;
                while (j > 0 && _recipeSteps[j].stepOrder.CompareTo(_recipeSteps[j - 1].stepOrder) < 0)
                {
                    Swap(_recipeSteps, j, j - 1);
                    j--;
                }
            }
        }

        public bool IsStateless { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(!IsStateless)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
