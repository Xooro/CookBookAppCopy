using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public Recipe SelectedRecipe { get; set; }
        public string Message { get; set; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SearchCommand { get; }

        RecipeServices recipeService;

        public RecipesViewModel()
        {
            recipeService = new RecipeServices();
            loadRecipe(new string[] { });
        }

        public void loadRecipe(string[] languages)
        {
            Task.Run(async () =>
            {
                var tempRecipes = await recipeService.getRecipesLocalized(languages);
                Recipes = new ObservableCollection<Recipe>(tempRecipes);
                Console.WriteLine("csumi");
            });
        }
    }
}
