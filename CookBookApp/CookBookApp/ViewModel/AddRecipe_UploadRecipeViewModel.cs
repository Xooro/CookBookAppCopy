using CookBookApp.Helpers;
using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CookBookApp.ViewModel
{
    public class AddRecipe_UploadRecipeViewModel : BaseViewModel
    {
        public bool IsBusy { get; set; }
        public bool IsUploadSuccessful { get; set; }
        public bool IsUploadFailed { get; set; }
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
        public Recipe NewRecipe { get; set; }

        UserSettingsManager userSettingsManager;
        RecipeService recipeService;

        int isBusyCounter;
        public AddRecipe_UploadRecipeViewModel(Recipe newRecipe)
        {
            userSettingsManager = new UserSettingsManager();
            recipeService = new RecipeService();

            isBusyCounter = 0;

            NewRecipe = newRecipe;
            uploadRecipe();
        }

        void uploadRecipe()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                Thread.Sleep(2000);
                bool uploadSuccess = await recipeService.uploadJoinedRecipeWithoutID(NewRecipe);
                if (uploadSuccess)
                    IsUploadSuccessful = true;
                else
                    IsUploadFailed = true;
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
