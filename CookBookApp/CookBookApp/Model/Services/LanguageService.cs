using CookBookApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.Model.Services
{
    public class LanguageService
    {
        RecipeContext _context;
        public LanguageService()
        {
            _context = new RecipeContext();
        }

        public LanguageService(RecipeContext context)
        {
            _context = context;
        }

        public async Task<List<Language>> getLanguagesAsync()
        {
            List<Language> languageResult = new List<Language>();
            try
            {
                languageResult = _context.Language.ToList();
            }
            catch(Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(languageResult);
        }

        public async Task<Language> getLanguageByIDAsync(int languageID)
        {
            Language languageResult = new Language();
            try
            {
                languageResult = _context.Language.Find(languageID);
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(languageResult);
        }

        public async Task<Language> getLanguageByNameAsync(string languageName)
        {
            languageName = languageName.ToUpper();
            Language languageResult = new Language();
            try
            {
                List<Language> languages = _context.Language.ToList();
                languageResult = languages.FirstOrDefault(l => l.LanguageName == languageName);
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(languageResult);
        }
    }
}
