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
        public RelayCommand CategoryChangedCommand { get; set; }

        int isBusyCounter;

        RecipeCategoriesService recipeCategoriesService;
        public UserSettingsManager UserSettingsManager;

        public EditRecipeViewModel(Recipe recipe)
        {

            isBusyCounter = 0;
            recipeCategoriesService = new RecipeCategoriesService();
            UserSettingsManager = new UserSettingsManager();

            Recipe = recipe;

            UserLanguage = UserSettingsManager.getLanguage();
            Difficulties = LocalizedConstants.getDifficulties();
            Prices = LocalizedConstants.getPrices();

            loadRecipeCategories();

            CategoryChangedCommand = new RelayCommand(changeCategories);
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

        void changeCategories()
        {
            var categoryNames = RecipeCategoryNames.Where(rcn => rcn.IsChecked).ToList();
            Recipe.Categories = categoryNames.Select(cn => {
                return new RecipeCategories
                {
                    CategoryNameID = cn.CategoryNameID,
                    RecipeID = Recipe.ID
                };
            }).ToList();
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
