using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Models.Services
{
    public class LanguageService
    {
        public async Task<List<Language>> GetLanguagesAsync()
        {
            List<Language> languageResult = new List<Language>();
            try
            {
                languageResult = await App._context.Languages.GetAllAsync();
            }
            catch(Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(languageResult);
        }
    }
}
