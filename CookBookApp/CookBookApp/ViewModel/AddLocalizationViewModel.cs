using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.ViewModel
{
    public class AddLocalizationViewModel : BaseViewModel
    {
        public Language SelectedLanguage { get; set; }
        public RecipeLocalization Localization { get; set; }
        public ObservableCollection<Language> Languages { get; set; }

        
        LanguageService languageService;

        public AddLocalizationViewModel(Recipe recipe)
        {
            languageService = new LanguageService();
            Localization = new RecipeLocalization { RecipeID = recipe.ID};

            loadLanguages();
        }

        void loadLanguages()
        {
            Task.Run(async () => { 
                Languages = new ObservableCollection<Language>(await languageService.getLanguagesAsync());
            });
            

        }
    }
}
