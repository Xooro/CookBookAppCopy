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
using System.Threading.Tasks;
using CookBookApp.Models.Services;

namespace CookBookApp.ViewModel
{
    public class AddRecipe_NgrdntsAndPrprtnVM : BaseViewModel
    {
        public bool IsBusy { get; set; }
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
        public Recipe NewRecipe { get; set; }
        public RelayCommand SendRecipeCommand { get; set; }

        UserSettingsManager userSettingsManager;

        int isBusyCounter;

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
