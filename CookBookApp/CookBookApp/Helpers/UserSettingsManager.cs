using CookBookApp.Data;
using CookBookApp.Models;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CookBookApp.Helpers
{
    public class UserSettingsManager
    {
        static UserSettings userProperties = new UserSettings();

        public string getUserName()
        {
            return userProperties.UserName;
        }

        public string getLanguage()
        {
            return userProperties.Language.ToUpper();
        }

        public async Task<bool> setUserName(string newUserName)
        {
            userProperties.UserName = newUserName;
            return await Task.FromResult(true);
        }

        public async Task<bool> setLanguage(Language newLanguage)
        {
            userProperties.Language = newLanguage.LanguageName;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(userProperties.Language);
            return await Task.FromResult(true);
        }
    }
}
