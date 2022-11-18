using CookBookApp.Models;
using CookBookApp.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Model.Services
{
    class RecipeCategoriesService
    {
        public async Task<List<RecipeCategoryNames>> getLocalizedRecipeCategoriesAsync(string languageName)
        {
            List<RecipeCategoryNames> recipeCategoryNames = new List<RecipeCategoryNames>();
            LanguageService languageService = new LanguageService();
            Language language = await languageService.getLanguageByName(languageName);
            
            try
            {
                recipeCategoryNames = await App._context.RecipeCategoryNames.GetAllAsync();
                recipeCategoryNames = recipeCategoryNames.Where(rcn => rcn.LanguageID == language.ID).ToList();
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(recipeCategoryNames);
        }
    }
}
