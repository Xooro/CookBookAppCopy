using CookBookApp.Helpers;
using CookBookApp.Model.Services;
using CookBookApp.Model;
using CookBookApp.Models;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CookBookApp.ViewModel
{
    class AddRecipe_NgrdntsAndPrprtnVM
    {
        UserSettingsManager userSettingsManager;

        int isBusyCounter;
        private int[] selectedCategoryNameIDs;

        public bool IsBusy { get; set; }
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
        public Recipe NewRecipe { get; set; }
        public RelayCommand SendRecipeCommand { get; set; }
        public RelayCommand UpdateCategoriesCommand { get; set; }

        public AddRecipe_NgrdntsAndPrprtnVM()
        {
            userSettingsManager = new UserSettingsManager();

            isBusyCounter = 0;


            MessagingCenter.Subscribe<AddRecipe_AlrgnsAndCtgrsVM, Recipe>(this, "NewRecipe",
                (page, newRecipe) =>
                {
                    NewRecipe = newRecipe;
                });
        }
    }
}
