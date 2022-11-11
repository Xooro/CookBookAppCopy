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
using Xamarin.Forms;

namespace CookBookApp.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public ObservableCollection<Language> SelectedLanguages { get; set; }
        public Recipe SelectedRecipe { get; set; }
        public string Message { get; set; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand TestCommnd { get;  }
        public bool IsBusy { get; set; }
        
        string search;
        
        
        RecipeService recipeService;
        LanguageService languageService;

        public RecipesViewModel()
        {
            recipeService = new RecipeService();
            languageService = new LanguageService();

            search = "";
            Languages = new ObservableCollection<Language>();
            SelectedLanguages = new ObservableCollection<Language>();

            loadLanguage();
            loadRecipe();
            TestCommnd = new RelayCommand(testFilter);
        }

        public void loadLanguage()
        {
            Task.Run(async () =>
            {
                Languages = new ObservableCollection<Language>(await languageService.GetLanguagesAsync());
            });
        }

        public void loadRecipe()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                Thread.Sleep(1000);
                var languageArray = SelectedLanguages.Select(l => l.LanguageName).ToArray();
                Recipes = new ObservableCollection<Recipe>(await recipeService.getRecipesLocalizedAsync(languageArray));

                IsBusy = false;
            });
        }

        public void testFilter()
        {
            SelectedLanguages = new ObservableCollection<Language>(Languages.Where(l => l.IsChecked).ToList());
            loadRecipe();
        }
    }
}
