using CookBookApp.Helpers;
using CookBookApp.Models.Services;
using CookBookApp.Models;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public string UserName { get; set; }
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

            loadLanguages();
            initializeUserSettings();

            SetUserNameCommand = new RelayCommand(setUserName);
            SetLanguageCommand = new RelayCommand(setLanguage);
        }

        void loadLanguages()
        {
            Task.Run(async () =>
            {
                Languages = new ObservableCollection<Language>(await languageService.getLanguagesAsync());
            }).Wait();
        }

        void initializeUserSettings()
        {
            UserName = userSettingsManager.getUserName();
            Task.Run(async () =>
            {
                Console.WriteLine(userSettingsManager.getLanguage());
                SelectedLanguage = userSettingsManager.getLanguage();
            });
        }

        async void setUserName()
        {
            await userSettingsManager.setUserName(UserName);
        }

        async void setLanguage()
        {
            await userSettingsManager.setLanguage(SelectedLanguage);
        }
    }
}
