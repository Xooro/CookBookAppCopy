using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
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
        public bool IsBusy { get; set; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SearchCommand { get; }
        public ICommand SearchCommandTest { get; set; }
        public RelayCommand FilterCommand { get;  }

        RecipeService recipeService;
        LanguageService languageService;

        string searchQuery;
        string[] selectedLanguages;

        public RecipesViewModel()
        {
            recipeService = new RecipeService();
            languageService = new LanguageService();

            searchQuery = "";
            selectedLanguages = new string[] { };

            loadLanguage();
            loadRecipe();

            FilterCommand = new RelayCommand(filter);
            SearchCommand = new RelayCommand(search);

            SearchCommandTest = new Command<string>(searchTest);
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
                Recipes = new ObservableCollection<Recipe>(await recipeService.getRecipesLocalizedAsync(selectedLanguages,searchQuery));

                IsBusy = false;
            });
        }

        private void filter()
        {
            searchQuery = "";
            List<Language> selectedLanguagesList = Languages.Where(l => l.IsChecked).ToList();
            selectedLanguages = selectedLanguagesList.Select(l => l.LanguageName).ToArray();
            loadRecipe();
        }

        //TODO: FIX SEARCH AND DELETE SEARCHTEST AFTER
        private void search()
        {
            loadRecipe();
        }

        private void searchTest(string searchQuery)
        {
            this.searchQuery = searchQuery;
            loadRecipe();
        }
    }
}
