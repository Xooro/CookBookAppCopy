using CookBookApp.Data;
using CookBookApp.Helpers;
using CookBookApp.Views;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBookApp
{
    public partial class App : Application
    {
        public static UserProperties userProperties = new UserProperties();
        static RecipeDatabase db; 
        public static RecipeDatabase _context
        {
            get
            {
                if (db == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                        "CookBookDB.db3");
                    db = new RecipeDatabase(path);
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();
            InitializeSettings();
            MainPage = new NavigationPage(new MenuPage());
        }

        public void InitializeSettings()
        {
            //ContextHelper.fillSQLiteWithTestData();
            setLanguage();
        }
        public void setLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("hu-HU");
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
