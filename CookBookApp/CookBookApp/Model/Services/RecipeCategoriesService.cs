using CookBookApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Model.Services
{
    class RecipeCategoriesService
    {
        public async Task<List<RecipeCategories>> GetRecipeCategoriesAsync()
        {
            List<RecipeCategories> recipeCategoriesResult = new List<RecipeCategories>();
            try
            {
                recipeCategoriesResult = await App._context.RecipeCategories.GetAllAsync();
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(recipeCategoriesResult);
        }
    }
}
