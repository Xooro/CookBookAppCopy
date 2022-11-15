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
        static SQLiteHelper db;
        public static UserProperties userProperties = new UserProperties();
        public static SQLiteHelper _context
        {
            get
            {
                if (db == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CookBookDB.db3");
                    //Console.WriteLine(path);
                    db = new SQLiteHelper(path);
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();
            InitializeSettings();
            //ContextHelper.fillSQLiteWithTestData();
            MainPage = new NavigationPage(new MenuPage());
        }

        public void InitializeSettings()
        {
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
