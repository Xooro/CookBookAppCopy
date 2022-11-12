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
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MenuPage());
            //ContextHelper.fillSQLiteWithTestData();
        }

        static SQLiteHelper db;

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
