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
    public class AddRecipeViewModel : BaseViewModel
    {
        public ObservableCollection<RecipeCategoryNames> RecipeCategoryNames { get; set; }

        RecipeCategoriesService recipeCategoriesService;
        UserSettingsManager userSettingsManager;

        int isBusyCounter;
        private int[] selectedCategoryNameIDs;

        public bool IsBusy { get; set; }
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
        public object PhotoPath { get; private set; }


        public Recipe NewRecipe { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand ForwardCommand { get; set; }
        public RelayCommand SelectImageCommand { get; set; }
        public RecipeImage TestImage { get; set; }
        public RelayCommand UpdateCategoriesCommand { get; set; }

        public AddRecipeViewModel()
        {
            recipeCategoriesService = new RecipeCategoriesService();
            userSettingsManager = new UserSettingsManager();

            NewRecipe = new Recipe();
            NewRecipe.LocalizedRecipe = new RecipeLocalization();
            NewRecipe.Categories = new List<RecipeCategories>();

            isBusyCounter = 0;

            initializeUserSettings();
            loadRecipeCategories();

            UpdateCategoriesCommand = new RelayCommand(UpdateCategories);
            SelectImageCommand = new RelayCommand(selectImage);
            TestImage = new RecipeImage();
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

        async void selectImage()
        {
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                byte[] result = new byte[stream.Length];
                stream.Read(result, 0, result.Length);
                TestImage.ImageBytes = result;
                Console.WriteLine(TestImage.Image);
                ImageSource image = ImageSource.FromStream(() => stream);
            }
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

        void UpdateCategories()
        {
            List<RecipeCategoryNames> selectedRecipeCategoryNames = RecipeCategoryNames.Where(rcn => rcn.IsChecked).ToList();
            selectedCategoryNameIDs = selectedRecipeCategoryNames.Select(rcn => rcn.CategoryNameID).ToArray();
        }
    }
}
