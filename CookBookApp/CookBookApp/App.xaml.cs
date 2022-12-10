using CookBookApp.Data;
using CookBookApp.Helpers;
using CookBookApp.Views;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBookApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeSettings();
            MainPage = new NavigationPage(new MenuPage());
        }

        public void InitializeSettings()
        {
            UserSettingsManager userPropertiesService = new UserSettingsManager();
            userPropertiesService.setAppLanguage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
