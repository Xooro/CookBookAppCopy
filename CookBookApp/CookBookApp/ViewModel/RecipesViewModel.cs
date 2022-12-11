using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Model.Services;
using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace CookBookApp.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public ObservableCollection<RecipeCategoryNames> RecipeCategoryNames { get; set; }

        public string Message { get; set; }
        public string SearchQuery { get; set; }
        public string UserName { get; set; }
        public Language UserLanguage { get; set; }
        public bool IsBusy { get; set; }

        public RelayCommand OpenCommand { get; set; }
        public RelayCommand<string> SearchCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand RefreshListCommand { get; set; }

        RecipesListService recipesListService;
        LanguageService languageService;
        RecipeCategoriesService recipeCategoriesService;

        UserSettingsManager userSettingsManager;

        int isBusyCounter;
        int[] selectedLanguageIDs;
        int[] selectedCategoryNameIDs;
        

        public RecipesViewModel()
        {
            recipesListService = new RecipesListService();
            languageService = new LanguageService();
            recipeCategoriesService = new RecipeCategoriesService();
            userSettingsManager = new UserSettingsManager();

            SearchQuery = "";
            isBusyCounter = 0;
            selectedLanguageIDs = new int[] { };
            selectedCategoryNameIDs = new int[] { };

            initializeUserSettings();
            
            loadLanguages();
            loadRecipeCategories();
            loadRecipes();

            FilterCommand = new RelayCommand(filter);
            SearchCommand = new RelayCommand<string>(search);
            RefreshListCommand = new RelayCommand(refreshList);
        }

        void initializeUserSettings()
        {
            UserName = userSettingsManager.getUserName();
            UserLanguage = userSettingsManager.getLanguage();
        }

        void loadLanguages()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                Languages = new ObservableCollection<Language>(
                    await languageService.getLanguagesAsync());
                Languages.FirstOrDefault(l => l.ID == UserLanguage.ID).IsChecked = true;
                selectedLanguageIDs = new int[] { UserLanguage.ID };
                setIsBusy(false);
            });

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

        void loadRecipes()
        {
            setIsBusy(true);
            Task.Run(async () =>
            {
                Thread.Sleep(1000);
                Recipes = new ObservableCollection<Recipe>(
                    await recipesListService.getRecipesLocalizedAsync(selectedCategoryNameIDs, selectedLanguageIDs, SearchQuery));
                setIsBusy(false);
            });
        }

        void filter()
        {
            //Azért szükséges, mert megnyomásakor  törli a searchban található adatot.
            //Az függ OneWay módban a SearchQuery-től. Ha a query "", akkor nem törli, ezért a kettős módosítás
            SearchQuery = "t";
            SearchQuery = "";

            List<RecipeCategoryNames> selectedRecipeCategoryNames = RecipeCategoryNames.Where(rcn => rcn.IsChecked).ToList();
            selectedCategoryNameIDs = selectedRecipeCategoryNames.Select(rcn => rcn.CategoryNameID).ToArray();

            List<Language> selectedLanguagesList = Languages.Where(l => l.IsChecked).ToList();
            selectedLanguageIDs = selectedLanguagesList.Select(l => l.ID).ToArray();
            loadRecipes();
        }

        void search(string searchQuery)
        {
            SearchQuery = searchQuery;
            loadRecipes();
        }

        void refreshList()
        {
            setIsBusy(true);
            recipesListService.setJoinedRecipes();
            loadRecipes();
            setIsBusy(false);
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
                IsBusy= false;
        }
    }
}
