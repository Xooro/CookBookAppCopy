using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CookBookApp.ViewModel
{
    public class ViewRecipeViewModel : BaseViewModel
    {
        public Recipe Recipe { get; set; }
        public Image Image { get; set; }
        public Language SelectedLanguage { get; set; }
        public RelayCommand ChangeLocalizationCommand { get; set; }

        RecipeService recipeService;

        public ViewRecipeViewModel(Recipe recipe)
        {
            Recipe = recipe;

            recipeService = new RecipeService();
            SelectedLanguage = Recipe.Languages.FirstOrDefault(r => r.ID == Recipe.LocalizedRecipe.LanguageID);

            ChangeLocalizationCommand = new RelayCommand(changeLocalization);
        }
        void changeLocalization()
        {
            Recipe recipe = recipeService.getLocalizedRecipeByRecipe(Recipe, SelectedLanguage.ID);
            Recipe = null;
            Recipe = recipe;
        }
    }
}
