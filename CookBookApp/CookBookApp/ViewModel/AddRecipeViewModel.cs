using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Model.Services;
using CookBookApp.Models;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CookBookApp.ViewModel
{
    public class AddRecipeViewModel : BaseViewModel
    {
        public ObservableCollection<RecipeCategoryNames> RecipeCategoryNames { get; set; }

        RecipeCategoriesService recipeCategoriesService;
        UserSettingsManager userSettingsManager;

        int isBusyCounter;
        public bool IsBusy { get; set; }
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
        public object PhotoPath { get; private set; }


        public Recipe NewRecipe { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand ForwardCommand { get; set; }



        public AddRecipeViewModel()
        {
            recipeCategoriesService = new RecipeCategoriesService();
            userSettingsManager = new UserSettingsManager();

            initializeUserSettings();
            loadRecipeCategories();

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
