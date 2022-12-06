﻿using CookBookApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CookBookApp.Data
{
    public class UserSettings
    {
        private string userName;
        private string language;
        private string path;
        public string UserName { get { return userName; } set { userName = value; updateUserProperties(); } }
        public string Language { get { return language; } set { language = value; updateUserProperties(); } }

        public UserSettings()
        {
            path = Path.Combine(Constants.path, "UserProperties.properties");
            if(!File.Exists(path) || File.ReadAllLines(path).Count()==0)
            {
                createDefaultFile();
            }
            initializeProperties();
        }

        private void createDefaultFile()
        {
            UserName = "User";
            Language = "EN";
        }

        private void initializeProperties()
        {
            try
            {
                var file = File.ReadLines(path).ToList();
                UserName = file[0].Split('=')[1];
                Language = file[1].Split('=')[1];
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message); ;
            }
        }

        private void updateUserProperties()
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine($"UserName={UserName}");
            sw.WriteLine($"Language={Language}");
            sw.Close();
        }
    }
}
