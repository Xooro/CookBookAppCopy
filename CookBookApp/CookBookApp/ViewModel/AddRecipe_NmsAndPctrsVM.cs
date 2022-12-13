using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Model.Interfaces;
using CookBookApp.Model.Services;
using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.Resources;
using CookBookApp.View;
using CookBookApp.ViewModels.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class AddRecipe_NmsAndPctrsVM : BaseViewModel
    {
        public string[] Difficulties { get; set; }
        public bool IsBusy { get; set; }
        public Recipe NewRecipe { get; set; }
        public ObservableCollection<RecipeImage> NewImages { get; set; }
        public RelayCommand SelectImageCommand { get; set; }
        public RelayCommand<RecipeImage> RemoveImageCommand { get; set; }

        UserSettingsManager userSettingsManager;
        RecipeService recipeService;
        int isBusyCounter;

        public AddRecipe_NmsAndPctrsVM()
        {
            userSettingsManager = new UserSettingsManager();
            recipeService = new RecipeService();

            NewRecipe = recipeService.getDefaultEmptyRecipe(userSettingsManager.getUserName(), userSettingsManager.getLanguage());
            Difficulties = LocalizedConstants.getDifficulties();
            NewImages = new ObservableCollection<RecipeImage>();

            isBusyCounter = 0;

            SelectImageCommand = new RelayCommand(selectImage);
            RemoveImageCommand = new RelayCommand<RecipeImage>(removeImage);
        }

        async void selectImage()
        {
            setIsBusy(true);
            byte[] result = await ImageHelper.selectImageAsByteArray();
            if (result == null)
            {
                //TODO: Message on page: error upload
                return;
            }     

            RecipeImage newImage = new RecipeImage();
            newImage.ImageBytes = result;

            NewImages.Add(newImage);
            NewRecipe.Images.Add(newImage);

            setIsBusy(false);
        }

        void removeImage(RecipeImage imageToRemove)
        {
            NewImages.Remove(imageToRemove);
            NewRecipe.Images.Remove(imageToRemove);
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
