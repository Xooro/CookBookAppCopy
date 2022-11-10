using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Models
{
    public class RecipeServices
    {
        public async Task<List<Recipe>> getRecipesByLocatization(string language)
        {
            List<Recipe> recipesResults = new List<Recipe>();
            try
            {
                var recipes = await App._context.Recipes.getAllAsync();
                var recipeLocalizations = await getRecipesLocalisationByLanguage(language);
                var recipeCategories = await getRecipesCategoriesByLangugae(language);
                var recipeImages = await App._context.RecipeImages.getAllAsync();
                
                foreach (Recipe recipe in recipes)
                {
                    recipe.Localization = recipeLocalizations.FirstOrDefault(rl => rl.RecipeID == recipe.ID);
                    recipe.Categories = recipeCategories.Where(rc => rc.RecipeID == recipe.ID).ToList();
                    recipe.Images = recipeImages.Where(ri => ri.RecipeID == recipe.ID).ToList();
                }
                recipesResults = recipes;
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }

            return await Task.FromResult(recipesResults);
        }

        private async Task<List<RecipeLocalization>> getRecipesLocalisationByLanguage(string language)
        {
            List<RecipeLocalization> recipesLocalizationResults = new List<RecipeLocalization>();
            try
            {
                var recipesLocalizations = await App._context.RecipeLocalizations.getAllAsync();
                recipesLocalizationResults = recipesLocalizations.Where(rl => rl.Language == language).ToList();
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(recipesLocalizationResults);
        }

        private async Task<List<RecipeCategories>> getRecipesCategoriesByLangugae(string language)
        {
            List<RecipeCategories> recipesCategoriesResults = new List<RecipeCategories>();
            try
            {
                var recipesCategoies = await App._context.RecipeCategories.getAllAsync();
                recipesCategoriesResults = recipesCategoies.Where(rl => rl.Language == language).ToList();
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(recipesCategoriesResults);
        }
    }
}
