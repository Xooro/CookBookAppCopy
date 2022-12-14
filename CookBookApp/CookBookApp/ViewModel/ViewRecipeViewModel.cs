using CookBookApp.Model;
using CookBookApp.Model.Services;
using CookBookApp.Resources;
using CookBookApp.ViewModels.Base;
using System.Linq;
using System;

namespace CookBookApp.ViewModel
{
    public class ViewRecipeViewModel : BaseViewModel
    {
        public Recipe Recipe { get; set; }
        public bool IsDefaultLocalization { get; set; }
        public bool IsRecipeLocalizationDeletable { get; set; }
        public Language SelectedLanguage { get; set; }
        public RelayCommand ChangeLocalizationCommand { get; set; }
        public RelayCommand DeleteRecippeLocalizationCommand { get; set; }
        public RelayCommand DeleteRecipeCommand { get; set; }

        RecipeService recipeService;

        public ViewRecipeViewModel(Recipe recipe)
        {
            Recipe = recipe;

            recipeService = new RecipeService();
            SelectedLanguage = Recipe.Languages.FirstOrDefault(r => r.ID == Recipe.LocalizedRecipe.LanguageID);

            ChangeLocalizationCommand = new RelayCommand(changeLocalization);
            DeleteRecippeLocalizationCommand = new RelayCommand(deleteRecipeLocalization);
            DeleteRecipeCommand = new RelayCommand(deleteRecipe);

            checkLocalizationIsDefaultOrDeletable();
        }

        void checkLocalizationIsDefaultOrDeletable()
        {
            if (SelectedLanguage.ID == Recipe.DefaultLanguageID)
                IsDefaultLocalization = true;
            else
                IsDefaultLocalization = false;

            if (!IsDefaultLocalization && Recipe.Languages.Count != 1)
                IsRecipeLocalizationDeletable = true;
            else
                IsRecipeLocalizationDeletable = false;
        }

        void changeLocalization()
        {
            Recipe recipe = recipeService.getLocalizedRecipeByRecipe(Recipe, SelectedLanguage.ID);
            Recipe = null;
            Recipe = recipe;
            checkLocalizationIsDefaultOrDeletable();
        }

        async void deleteRecipeLocalization()
        {
            bool isDeleted = await recipeService.deleteRecipeLocalizationAsync(Recipe.LocalizedRecipe);
            if (isDeleted)
                await App.Current.MainPage.DisplayAlert(AppResources.CONS_Message, AppResources.CONS_SuccessfulLocalizationDelete, "OK");
            else
                await App.Current.MainPage.DisplayAlert(AppResources.CONS_Message, AppResources.CONS_FailedDelete, "OK");
        }

        async void deleteRecipe()
        {
            bool isDeleted = await recipeService.deleteRecipeAsync(Recipe);
            if (isDeleted)
                await App.Current.MainPage.DisplayAlert(AppResources.CONS_Message, AppResources.CONS_SuccessfulDelete, "OK");
            else
                await App.Current.MainPage.DisplayAlert(AppResources.CONS_Message, AppResources.CONS_FailedDelete, "OK");
        }

    }
}
