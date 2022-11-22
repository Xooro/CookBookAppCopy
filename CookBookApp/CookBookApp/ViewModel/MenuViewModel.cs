using CookBookApp.Helpers;
using CookBookApp.Helpers.Temporary;
using CookBookApp.Models;
using CookBookApp.Models.Services;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CookBookApp.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public RelayCommand<string> SetUserNameCommand { get; set; }
        public RelayCommand SetLanguageCommand { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        public Language SelectedLanguage { get; set; }

        LanguageService languageService;
        UserSettingsManager userPropertiesService;

        public MenuViewModel()
        {
            languageService = new LanguageService();
            userPropertiesService = new UserSettingsManager();

            loadLanguages();
            ContextHelper.fillSQLiteWithTestData();
            SetUserNameCommand = new RelayCommand<string>(setUserName);
            SetLanguageCommand = new RelayCommand(setLanguage);
        }

        void loadLanguages()
        {
            Task.Run(async () =>
            {
                Languages = new ObservableCollection<Language>(await languageService.getLanguagesAsync());
            });
        }

        async void setUserName(string userName)
        {
            await userPropertiesService.setUserName(userName);
        }

        async void setLanguage()
        {
            await userPropertiesService.setLanguage(SelectedLanguage);
        }
    }
}
