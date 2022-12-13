using CookBookApp.Helpers;
using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.Resources;
using CookBookApp.ViewModels.Base;
using System.Threading;
using System.Threading.Tasks;

namespace CookBookApp.ViewModel
{
    public class _AddOrEditRecipeViewModel : BaseViewModel
    {
        public bool IsBusy { get; set; }
        public bool IsUploadSuccessful { get; set; }
        public bool IsUploadFailed { get; set; }
        public string UploadMessage { get; set; }
        public Recipe Recipe { get; set; }
        bool isNew;

        RecipeService recipeService;

        int isBusyCounter;
        public _AddOrEditRecipeViewModel(Recipe recipe)
        {
            recipeService = new RecipeService();

            isBusyCounter = 0;

            Recipe = recipe;
            isNew = recipe.ID == 0;

            if (isNew)
                uploadRecipe();
            else
                updateRecipe();
        }

        void uploadRecipe()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                Thread.Sleep(1000);
                bool uploadSuccess = await recipeService.uploadJoinedRecipeWithoutIDAsync(Recipe);
                if (uploadSuccess)
                {
                    IsUploadSuccessful = true;
                    UploadMessage = AppResources.CONS_SuccessfulUpload;
                } 
                else
                {
                    IsUploadFailed = true;
                    UploadMessage = AppResources.CONS_FailedUpload;
                }
                    
                refreshRecipe();
                setIsBusy(false);
            });
        }

        void updateRecipe()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                Thread.Sleep(1000);
                bool uploadSuccess = await recipeService.updateLocalizedRecipeAsync(Recipe);
                if (uploadSuccess)
                {
                    IsUploadSuccessful = true;
                    UploadMessage = AppResources.CONS_SuccessfulUpdate;
                }
                else
                {
                    IsUploadFailed = true;
                    UploadMessage = AppResources.CONS_FailedUpdate;
                }

                refreshRecipe();
                setIsBusy(false);
            });
        }

        void refreshRecipe()
        {
            Recipe = recipeService.getLocalizedRecipeByRecipe(Recipe, Recipe.LocalizedRecipe.LanguageID);
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
