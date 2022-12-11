using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Model.Services;
using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.ViewModel
{
    public class EditRecipeViewModel : BaseViewModel
    {
        public string[] Difficulties { get; set; }
        public string[] Prices { get; set; }
        public bool IsBusy { get; set; }
        public Recipe Recipe { get; set; }
        public Language UserLanguage { get; set; }
        public ObservableCollection<RecipeCategoryNames> RecipeCategoryNames { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public RelayCommand DeleteRecipeCommand { get; set; }
        public RelayCommand UpdateRecipeCommand { get; set; }


        int isBusyCounter;


        RecipeCategoriesService recipeCategoriesService;
        RecipeService recipeService;
        public UserSettingsManager UserSettingsManager;


        public EditRecipeViewModel(Recipe recipe)
        {

            isBusyCounter = 0;
            recipeCategoriesService = new RecipeCategoriesService();
            recipeService = new RecipeService();
            UserSettingsManager = new UserSettingsManager();

            Recipe = recipe;

            UserLanguage = UserSettingsManager.getLanguage();
            Difficulties = LocalizedConstants.getDifficulties();
            Prices = LocalizedConstants.getPrices();


            loadRecipeCategories();

            DeleteRecipeCommand = new RelayCommand(deleteRecipe);
            UpdateRecipeCommand = new RelayCommand(updateRecipe);
        }
        void loadRecipeCategories()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                var recipeCategoryNames = await recipeCategoriesService.getLocalizedRecipeCategoriesAsync(UserLanguage);
                var recipeCategoryIDs = Recipe.Categories.Select(rc => rc.CategoryNameID).ToArray();
                recipeCategoryNames.Select(rcn => { if (recipeCategoryIDs.Contains(rcn.CategoryNameID)) rcn.IsChecked = true; return rcn; }).ToList();

                RecipeCategoryNames = new ObservableCollection<RecipeCategoryNames>(recipeCategoryNames);
                setIsBusy(false);
            });

        }
        async void deleteRecipe()
        {
            await recipeService.deleteRecipeAsync(Recipe);
        }

        async void updateRecipe()
        {
            await recipeService.updateRecipe(Recipe);
        }

        void setIsBusy(bool toTrue)
        {
            if (toTrue)
                isBusyCounter++;
            else
                isBusyCounter--;

            if (isBusyCounter > 0)
                IsBusy = true;
            else
                IsBusy = false;
        }
    }
}
