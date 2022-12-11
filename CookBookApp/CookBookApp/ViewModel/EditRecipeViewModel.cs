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
        RecipeCategoriesService recipeCategoriesService;
        public UserSettingsManager UserSettingsManager;
        public string[] Difficulties { get; set; }
        public string[] Prices { get; set; }
        public bool IsBusy { get; set; }
        public Recipe OriginalRecipe { get; set; }
        public Recipe EditedRecipe { get; set; }
        public Language UserLanguage { get; set; }

        int isBusyCounter;

        public ObservableCollection<RecipeCategoryNames> RecipeCategoryNames { get; set; }
        public ObservableCollection<Language> Languages { get; set; }


        public EditRecipeViewModel(Recipe recipe)
        {

            isBusyCounter = 0;
            recipeCategoriesService = new RecipeCategoriesService();
            UserSettingsManager = new UserSettingsManager();

            OriginalRecipe = recipe;
            EditedRecipe= recipe;

            UserLanguage = UserSettingsManager.getLanguage();
            Difficulties = LocalizedConstants.getDifficulties();
            Prices = LocalizedConstants.getPrices();


            loadRecipeCategories();
        }
        void loadRecipeCategories()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                var recipeCategoryNames = await recipeCategoriesService.getLocalizedRecipeCategoriesAsync(UserLanguage);
                var recipeCategoryIDs = OriginalRecipe.Categories.Select(rc => rc.CategoryNameID).ToArray();
                recipeCategoryNames.Select(rcn => { if (recipeCategoryIDs.Contains(rcn.CategoryNameID)) rcn.IsChecked = true; return rcn; }).ToList();

                RecipeCategoryNames = new ObservableCollection<RecipeCategoryNames>(recipeCategoryNames);
                setIsBusy(false);
            });

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
