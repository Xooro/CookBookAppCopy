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
    public class AddRecipe_NgrdntsAndPrprtnVM : BaseViewModel
    {
        UserSettingsManager userSettingsManager;

        int isBusyCounter;

        public bool IsBusy { get; set; }
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
        public Recipe NewRecipe { get; set; }
        public RelayCommand SendRecipeCommand { get; set; }

        public AddRecipe_NgrdntsAndPrprtnVM()
        {
            userSettingsManager = new UserSettingsManager();

            isBusyCounter = 0;

            MessagingCenter.Subscribe<AddRecipe_AlrgnsAndCtgrsVM, Recipe>(this, "NewRecipeToIngAndPrepVM",
                (page, newRecipe) =>
                {
                    NewRecipe = newRecipe;
                });
        }
    }
}
