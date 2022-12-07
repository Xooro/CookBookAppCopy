using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Model.Interfaces;
using CookBookApp.Model.Services;
using CookBookApp.Models;
using CookBookApp.Models.Services;
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
        public bool IsBusy { get; set; }
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
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

            initializeUserSettings();
            NewRecipe = recipeService.getDefaultEmptyRecipe(UserName, UserLanguage);
            NewImages = new ObservableCollection<RecipeImage>();

            isBusyCounter = 0;

            SelectImageCommand = new RelayCommand(selectImage);
            RemoveImageCommand = new RelayCommand<RecipeImage>(removeImage);
        }

        void initializeUserSettings()
        {
            UserName = userSettingsManager.getUserName();
            UserLanguage = userSettingsManager.getLanguage();
        }

        async void selectImage()
        {
            setIsBusy(true);
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                byte[] result = new byte[stream.Length];
                stream.Read(result, 0, result.Length);

                RecipeImage newImage = new RecipeImage();
                newImage.ImageBytes = result;

                NewImages.Add(newImage);
                NewRecipe.Images.Add(newImage);
            }
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
