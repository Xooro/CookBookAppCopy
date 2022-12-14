using CookBookApp.Helpers;
using CookBookApp.Model.Services;
using CookBookApp.Model;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CookBookApp.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public Language UserLanguage { get; set; }
        public RelayCommand SetUserNameCommand { get; set; }
        public RelayCommand SetLanguageCommand { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public Language SelectedLanguage { get; set; }

        LanguageService languageService;
        UserSettingsManager userSettingsManager;

        public SettingsViewModel()
        {
            languageService = new LanguageService();
            userSettingsManager = new UserSettingsManager();

            initializeUserSettings();
            loadLanguages();

            SetUserNameCommand = new RelayCommand(setUserName);
            SetLanguageCommand = new RelayCommand(setLanguage);
        }

        void initializeUserSettings()
        {
            UserName = userSettingsManager.getUserName();
            UserLanguage = userSettingsManager.getLanguage();
        }

        async void loadLanguages()
        {
            Languages = new ObservableCollection<Language>(await languageService.getLanguagesAsync());
            SelectedLanguage = Languages.FirstOrDefault(l => l.ID == UserLanguage.ID);
        }

        async void setUserName()
        {
            await userSettingsManager.setUserName(UserName);
        }

        async void setLanguage()
        {
            await userSettingsManager.setUserLanguage(SelectedLanguage);
        }
    }
}
