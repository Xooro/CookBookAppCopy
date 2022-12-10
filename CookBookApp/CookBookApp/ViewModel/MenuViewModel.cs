using CookBookApp.Data;
using CookBookApp.Helpers;
using CookBookApp.Models;
using CookBookApp.ViewModels.Base;
using System.Linq;
using Xamarin.Forms;

namespace CookBookApp.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public Language UserLanguage { get; set; }
        
        UserSettingsManager userSettingsManager;
        
        public MenuViewModel()
        { 
            userSettingsManager = new UserSettingsManager();
            initializeUserSettings();
        }

        void initializeUserSettings()
        {
            UserName = userSettingsManager.getUserName();
            UserLanguage = userSettingsManager.getLanguage();
        }
    }
}
