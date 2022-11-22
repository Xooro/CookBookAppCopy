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
            //ContextHelper.fillSQLiteWithTestData();
            UserSettingsManager userPropertiesService = new UserSettingsManager();
            setLanguage(userPropertiesService.getLanguage());
        }
        public void setLanguage(string languageName)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(languageName);
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
