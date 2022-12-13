using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.Resources;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.ViewModel
{
    public class AddLocalizationViewModel : BaseViewModel
    {
        Language selectedLanguage;
        public Language SelectedLanguage {
            get 
            { 
                return selectedLanguage;
            } 
            set {
                if (value == null)
                    selectedLanguage = new Language();
                else
                    selectedLanguage = value;
            }}
        public RecipeLocalization RecipeLocalization { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public Recipe Recipe { get; set; }
        public RelayCommand ChangeLocLangCommand { get; set; }
        public RelayCommand UploadLocalizationCommand { get; set; }

        RecipeService recipeService;
        LanguageService languageService;

        public AddLocalizationViewModel(Recipe recipe)
        {
            recipeService = new RecipeService();
            languageService = new LanguageService();

            Recipe = recipe;
            RecipeLocalization = new RecipeLocalization { RecipeID = Recipe.ID};

            loadLanguages();

            ChangeLocLangCommand = new RelayCommand(changeLocalizationLanguage);
            UploadLocalizationCommand = new RelayCommand(uploadLocalization);
        }

        void loadLanguages()
        {
            Task.Run(async () => {
                var languages = await languageService.getLanguagesAsync();
                var languageIDs = Recipe.Languages.Select(rl => rl.ID).ToArray();
                languages = languages.Where(l => !languageIDs.Contains(l.ID)).ToList();
                
                Languages = new ObservableCollection<Language>(languages);
                SelectedLanguage = Languages.First();
            });
        }

        void changeLocalizationLanguage()
        {
            RecipeLocalization.LanguageID = SelectedLanguage.ID;
        }

        async void uploadLocalization()
        {
            bool isUploaded = recipeService.addRecipeLocalization(RecipeLocalization);
            if (isUploaded)
                await App.Current.MainPage.DisplayAlert(AppResources.CONS_Message, AppResources.CONS_SuccessfulUpload, "OK");
            else
                await App.Current.MainPage.DisplayAlert(AppResources.CONS_Message, AppResources.CONS_FailedUpload, "OK");
            refreshPage();
        }

        void refreshPage()
        {
            Recipe = recipeService.getLocalizedRecipeByRecipe(Recipe, Recipe.LocalizedRecipe.LanguageID);
            loadLanguages();
        }
    }
}
