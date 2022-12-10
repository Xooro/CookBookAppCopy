using CookBookApp.Data;
using CookBookApp.Models;
using CookBookApp.Models.Services;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CookBookApp.Helpers
{
    public class UserSettingsManager
    {
        static UserSettings userProperties = new UserSettings();
        LanguageService languageService;
        public UserSettingsManager()
        {
            languageService= new LanguageService();
        }

        public string getUserName()
        {
            return userProperties.UserName;
        }

        public Language getLanguage()
        {
            int languageID = userProperties.LanguageID;
            Language languageResult = new Language();
            Task.Run(async () => {
                languageResult = await languageService.getLanguageByIDAsync(languageID);
            }).Wait();
            return languageResult;
        }

        public async Task<bool> setUserName(string newUserName)
        {
            userProperties.UserName = newUserName;
            return await Task.FromResult(true);
        }

        public async Task<bool> setLanguage(Language newLanguage)
        {
            userProperties.LanguageID = newLanguage.ID;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(newLanguage.LanguageName);
            return await Task.FromResult(true);
        }
    }
}
