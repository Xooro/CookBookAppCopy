using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System.Linq;

namespace CookBookApp.ViewModel
{
    public class ViewRecipeViewModel : BaseViewModel
    {
        public Recipe Recipe { get; set; }
        public Language SelectedLanguage { get; set; }
        public RelayCommand ChangeLocalizationCommand { get; set; }
        public RelayCommand DeleteRecipeCommand { get; set; }

        RecipeService recipeService;

        public ViewRecipeViewModel(Recipe recipe)
        {
            Recipe = recipe;

            recipeService = new RecipeService();
            SelectedLanguage = Recipe.Languages.FirstOrDefault(r => r.ID == Recipe.LocalizedRecipe.LanguageID);

            ChangeLocalizationCommand = new RelayCommand(changeLocalization);
            DeleteRecipeCommand = new RelayCommand(deleteRecipe);
        }
        void changeLocalization()
        {
            Recipe recipe = recipeService.getLocalizedRecipeByRecipe(Recipe, SelectedLanguage.ID);
            Recipe = null;
            Recipe = recipe;
        }

        async void deleteRecipe()
        {
            bool isDeleted = await recipeService.deleteRecipeAsync(Recipe);
            await App.Current.MainPage.DisplayAlert("Message", "Successful delete", "OK");
        }

    }
}
