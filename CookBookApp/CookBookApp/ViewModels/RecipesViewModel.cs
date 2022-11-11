using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
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
        public RelayCommand TestCommnd { get;  }
        public bool IsBusy { get; set; } = false;


        RecipeServices recipeService;

        public RecipesViewModel()
        {
            recipeService = new RecipeServices();
            loadRecipe(new string[] { });
            TestCommnd = new RelayCommand(testFilter);
        }

        public void loadRecipe(string[] languages)
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                Thread.Sleep(2000);
                Recipes = new ObservableCollection<Recipe>(await recipeService.getRecipesLocalized(languages));

                IsBusy = false;
            });
            
        }

        public void testFilter()
        {
            loadRecipe(new string[] { "en" });
        }
    }
}
