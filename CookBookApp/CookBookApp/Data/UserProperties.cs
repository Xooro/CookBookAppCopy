using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CookBookApp.Data
{
    public class UserProperties
    {
        private string userName;
        private string language;
        public string UserName { get { return userName; } set { userName = value; UpdateUserProperties(); } }
        public string Language { get { return language; } set { language = value; UpdateUserProperties(); } }

        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserProperties.properties");


        public UserProperties()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserProperties.properties");

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

        
        private void UpdateUserProperties()
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine($"UserName={UserName}");
            sw.WriteLine($"Language={Language}");
            sw.Close();
        }
    }
}
