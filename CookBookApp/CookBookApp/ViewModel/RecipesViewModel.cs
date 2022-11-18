using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Model.Services;
using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CookBookApp.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public ObservableCollection<RecipeCategoryNames> RecipeCategoryNames{ get; set; }
        public Recipe SelectedRecipe { get; set; }
        
        public string Message { get; set; }
        public string SearchQuery { get; set; }
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
        public bool IsBusy { get; set; }

        public RelayCommand OpenCommand { get; }
        public RelayCommand<string> SearchCommand { get; }
        public RelayCommand FilterCommand { get;  }

        RecipeService recipeService;
        LanguageService languageService;
        RecipeCategoriesService recipeCategoriesService;

        UserSettingsManager userSettingsManager;

        string[] selectedLanguages;
        int[] selectedCategoryNameIDs;

        public RecipesViewModel()
        {
            recipeService = new RecipeService();
            languageService = new LanguageService();
            recipeCategoriesService = new RecipeCategoriesService();
            userSettingsManager = new UserSettingsManager();

            SearchQuery = "";
            selectedLanguages = new string[] { };
            selectedCategoryNameIDs = new int[] { };
            
            UserName = userSettingsManager.getUserName();
            UserLanguage = userSettingsManager.getLanguage();

            loadLanguages();
            loadRecipes();
            loadRecipeCategories();

            FilterCommand = new RelayCommand(filter);
            SearchCommand = new RelayCommand<string>(search);
        }

        private void loadLanguages()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                Languages = new ObservableCollection<Language>(await languageService.getLanguagesAsync());
            });
            IsBusy = false;
        }

        private void loadRecipes()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                Recipes = new ObservableCollection<Recipe>(await recipeService.getRecipesLocalizedAsync(selectedCategoryNameIDs, selectedLanguages, SearchQuery));

                IsBusy = false;
            });
        }

        private void loadRecipeCategories()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                RecipeCategoryNames = new ObservableCollection<RecipeCategoryNames>(await recipeCategoriesService.getLocalizedRecipeCategoriesAsync(UserLanguage));
            });
            IsBusy = false;
        }

        private void filter()
        {
            //ez azért szükséges, mert megnyomásakor  törli a searchban található adatot.
            //Az függ OneWay módban a SearchQuery-től. Ha a query "", akkor nem törli, ezért a kettős módosítás
            SearchQuery = "t";
            SearchQuery = "";

            List<RecipeCategoryNames> selectedRecipeCategoryNames = RecipeCategoryNames.Where(rcn => rcn.IsChecked).ToList();
            selectedCategoryNameIDs = selectedRecipeCategoryNames.Select(rcn => rcn.CategoryNameID).ToArray();

            List<Language> selectedLanguagesList = Languages.Where(l => l.IsChecked).ToList();
            selectedLanguages = selectedLanguagesList.Select(l => l.LanguageName).ToArray();
            loadRecipes();
        }

        private void search(string searchQuery)
        {
            this.SearchQuery = searchQuery;
            loadRecipes();
        }
    }
}
