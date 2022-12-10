using CookBookApp.Data;
using CookBookApp.Models;
using CookBookApp.Models.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Model.Services
{
    class RecipeCategoriesService
    {
        RecipeContext _context;
        public RecipeCategoriesService()
        {
            _context = new RecipeContext();
        }

        public RecipeCategoriesService(RecipeContext context)
        {
            _context = context;
        }

        public async Task<List<RecipeCategoryNames>> getLocalizedRecipeCategoriesAsync(Language language)
        {
            List<RecipeCategoryNames> recipeCategoryNames = new List<RecipeCategoryNames>(); 
            try
            {
                recipeCategoryNames = _context.RecipeCategoryNames.ToList();
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
