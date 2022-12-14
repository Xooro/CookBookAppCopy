using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Model.Services;
using CookBookApp.Model;
using CookBookApp.Model.Services;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.ViewModel
{
    public class EditRecipeViewModel : BaseViewModel
    {
        public string[] Difficulties { get; set; }
        public string[] Prices { get; set; }
        public bool IsBusy { get; set; }
        public Recipe Recipe { get; set; }
        public TimeSpan PreparationTime
        {
            get
            {
                return Recipe.PreparationTime.TimeOfDay;
            }
            set
            {
                Recipe.PreparationTime = new DateTime() + value;
            }
        }
        public ObservableCollection<RecipeCategoryNames> RecipeCategoryNames { get; set; }
        public ObservableCollection<RecipeImage> RecipeImages { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public RelayCommand SelectImageCommand { get; set; }
        public RelayCommand<RecipeImage> RemoveImageCommand { get; set; }
        public RelayCommand CategoryChangedCommand { get; set; }

        Language userLanguage;
        int isBusyCounter;

        RecipeCategoriesService recipeCategoriesService;
        public UserSettingsManager userSettingsManager;

        public EditRecipeViewModel(Recipe recipe)
        {

            isBusyCounter = 0;
            recipeCategoriesService = new RecipeCategoriesService();
            userSettingsManager = new UserSettingsManager();

            Recipe = recipe;

            userLanguage = userSettingsManager.getLanguage();
            Difficulties = LocalizedConstants.getDifficulties();
            Prices = LocalizedConstants.getPrices();

            loadRecipeCategories();
            loadRecipeImages();

            SelectImageCommand = new RelayCommand(selectImage);
            RemoveImageCommand = new RelayCommand<RecipeImage>(removeImage);
            CategoryChangedCommand = new RelayCommand(changeCategories);
        }
        void loadRecipeCategories()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                var recipeCategoryNames = await recipeCategoriesService.getLocalizedRecipeCategoriesAsync(userLanguage);
                var recipeCategoryIDs = Recipe.Categories.Select(rc => rc.CategoryNameID).ToArray();
                recipeCategoryNames.Select(rcn => { if (recipeCategoryIDs.Contains(rcn.CategoryNameID)) rcn.IsChecked = true; return rcn; }).ToList();

                RecipeCategoryNames = new ObservableCollection<RecipeCategoryNames>(recipeCategoryNames);
                setIsBusy(false);
            });

        }

        void loadRecipeImages()
        {
            RecipeImages = new ObservableCollection<RecipeImage>(Recipe.Images);
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
            newImage.RecipeID = Recipe.ID;
            newImage.ImageBytes = result;

            RecipeImages.Add(newImage);
            Recipe.Images.Add(newImage);

            setIsBusy(false);
        }

        void removeImage(RecipeImage imageToRemove)
        {
            RecipeImages.Remove(imageToRemove);
            Recipe.Images.Remove(imageToRemove);
        }

        void changeCategories()
        {
            var categoryNames = RecipeCategoryNames.Where(rcn => rcn.IsChecked).ToList();
            Recipe.Categories = categoryNames.Select(cn => {
                return new RecipeCategories
                {
                    CategoryNameID = cn.CategoryNameID,
                    RecipeID = Recipe.ID
                };
            }).ToList();
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
