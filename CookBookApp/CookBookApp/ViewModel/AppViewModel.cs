using CookBookApp.Helpers;
using CookBookApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookApp.ViewModel
{
    public class AppViewModel
    {
        public string UserName { get; set; }
        public Language UserLanguage { get; set; }
        UserSettingsManager userSettingsManager;

        public AppViewModel()
        {
            userSettingsManager = new UserSettingsManager();

            UserName = userSettingsManager.getUserName();
            UserLanguage = userSettingsManager.getLanguage();

            initializeSettings();
        }

        void initializeSettings()
        {
            userSettingsManager.setAppLanguage();
        }
    }
}
