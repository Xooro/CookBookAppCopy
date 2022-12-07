using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Model.Interfaces;
using CookBookApp.Model.Services;
using CookBookApp.Models;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CookBookApp.ViewModel
{
    public class AddRecipe_AlrgnsAndCtgrsVM : BaseViewModel
    {
        public ObservableCollection<RecipeCategoryNames> RecipeCategoryNames { get; set; }

        RecipeCategoriesService recipeCategoriesService;
        UserSettingsManager userSettingsManager;

        int isBusyCounter;
        private int[] selectedCategoryNameIDs;

        public bool IsBusy { get; set; }
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
        public Recipe NewRecipe { get; set; }
        public RelayCommand SendRecipeCommand { get; set; }
        public RelayCommand UpdateCategoriesCommand { get; set; }

        public AddRecipe_AlrgnsAndCtgrsVM()
        {
            recipeCategoriesService = new RecipeCategoriesService();
            userSettingsManager = new UserSettingsManager();

            isBusyCounter = 0;

            initializeUserSettings();
            loadRecipeCategories();

            MessagingCenter.Subscribe<AddRecipe_NmsAndPctrsVM, Recipe>(this, "NewRecipe",
                (page, newRecipe) =>
                {
                    NewRecipe = newRecipe;
                });

            UpdateCategoriesCommand = new RelayCommand(UpdateCategories);
        }

        void initializeUserSettings()
        {
            UserName = userSettingsManager.getUserName();
            UserLanguage = userSettingsManager.getLanguage();
        }
        void loadRecipeCategories()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                RecipeCategoryNames = new ObservableCollection<RecipeCategoryNames>(
                    await recipeCategoriesService.getLocalizedRecipeCategoriesAsync(UserLanguage));
                setIsBusy(false);
            });
        }

        void UpdateCategories()
        {
            List<RecipeCategoryNames> selectedRecipeCategoryNames = RecipeCategoryNames.Where(rcn => rcn.IsChecked).ToList();
            selectedCategoryNameIDs = selectedRecipeCategoryNames.Select(rcn => rcn.CategoryNameID).ToArray();
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
