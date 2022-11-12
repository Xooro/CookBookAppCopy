using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using CookBookApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookBookApp.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public Recipe SelectedRecipe { get; set; }
        public string Message { get; set; }
        public string SearchQuery { get; set; }
        public bool IsBusy { get; set; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand<string> SearchCommand { get; }
        public RelayCommand FilterCommand { get;  }

        RecipeService recipeService;
        LanguageService languageService;

        
        string[] selectedLanguages;

        public RecipesViewModel()
        {
            recipeService = new RecipeService();
            languageService = new LanguageService();

            SearchQuery = "";
            selectedLanguages = new string[] { };

            loadLanguage();
            loadRecipe();

            FilterCommand = new RelayCommand(filter);
            SearchCommand = new RelayCommand<string>(search);
        }

        private void loadLanguage()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                Languages = new ObservableCollection<Language>(await languageService.GetLanguagesAsync());
            });
            IsBusy = false;
        }

        private void loadRecipe()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                Thread.Sleep(1000);
                Recipes = new ObservableCollection<Recipe>(await recipeService.getRecipesLocalizedAsync(selectedLanguages,SearchQuery));

                IsBusy = false;
            });
        }

        private void filter()
        {
            //ez azért szükséges, mert megnyomásakor  törli a searchban található adatot.
            //Az függ OneWay módban a SearchQuery-től. Ha a query "", akkor nem törli, ezért a kettős módosítás
            SearchQuery = "t";
            SearchQuery = "";
            
            List<Language> selectedLanguagesList = Languages.Where(l => l.IsChecked).ToList();
            selectedLanguages = selectedLanguagesList.Select(l => l.LanguageName).ToArray();
            loadRecipe();
        }

        private void search(string searchQuery)
        {
            this.SearchQuery = searchQuery;
            loadRecipe();
        }
    }
}
