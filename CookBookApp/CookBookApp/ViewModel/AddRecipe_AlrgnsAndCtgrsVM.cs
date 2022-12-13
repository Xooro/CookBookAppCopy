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
        public string[] Prices { get; set; }
        public bool IsBusy { get; set; }
        public Recipe NewRecipe { get; set; }
        public TimeSpan PreparationTime
        {
            get
            {
                return NewRecipe.PreparationTime.TimeOfDay;
            }
            set
            {
                NewRecipe.PreparationTime = new DateTime() + value;
            } 
        }
        public RelayCommand UpdateCategoriesCommand { get; set; }

        int isBusyCounter;

        RecipeCategoriesService recipeCategoriesService;
        UserSettingsManager userSettingsManager;

        public AddRecipe_AlrgnsAndCtgrsVM(Recipe newRecipe)
        {
            recipeCategoriesService = new RecipeCategoriesService();
            userSettingsManager = new UserSettingsManager();

            Prices = LocalizedConstants.getPrices();

            isBusyCounter = 0;

            loadRecipeCategories();

            NewRecipe = newRecipe;

            UpdateCategoriesCommand = new RelayCommand(updateCategories);
        }
        void loadRecipeCategories()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                RecipeCategoryNames = new ObservableCollection<RecipeCategoryNames>(
                    await recipeCategoriesService.getLocalizedRecipeCategoriesAsync(userSettingsManager.getLanguage()));
                setIsBusy(false);
            });
        }

        void updateCategories()
        {
            setIsBusy(true);
            var categoriesToAdd = (from rcn in RecipeCategoryNames
                                  where rcn.IsChecked
                                  select new RecipeCategories { CategoryNameID = rcn.CategoryNameID }).ToList();
            NewRecipe.Categories = categoriesToAdd;
            setIsBusy(false);
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
