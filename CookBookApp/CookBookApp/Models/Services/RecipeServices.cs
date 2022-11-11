using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Models.Services
{
    public class RecipeServices
    {
        public async Task<List<Recipe>> getRecipesJoinedByLanguage(string language)
        {
            language = language.ToUpper();
            List<Recipe> recipesResults = new List<Recipe>();
            try
            {
                var recipes = await getRecipesJoined();
                var recipesBuff = new List<Recipe>();
                var languages = await App._context.Languages.GetAllAsync();
                int languageID = languages.FirstOrDefault(l => l.LanguageName == language).ID;

                foreach (Recipe recipe in recipes)
                {
                    if (recipe.Languages.Any(l => l.LanguageName == language))
                    {
                        Recipe recipeBuff = recipe;
                        recipeBuff.LocalizedRecipe = recipe.Localizations.FirstOrDefault(l => l.LanguageID == languageID);
                        recipeBuff.LocalizedCategories = recipe.Categories.Where(c => c.LanguageID == languageID).ToList();
                        recipeBuff.Localizations = null;
                        recipeBuff.Categories = null;

                        recipesBuff.Add(recipeBuff);
                    }
                }

                recipesResults = recipesBuff;
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            
            return await Task.FromResult(recipesResults);
        }

        public async Task<List<Recipe>> getRecipesJoined()
        {
            List<Recipe> recipesResults = new List<Recipe>();
            try
            {
                var recipes = await App._context.Recipes.GetAllAsync();
                var recipeLocalizations = await App._context.RecipeLocalizations.GetAllAsync();
                var recipeCategories = await App._context.RecipeCategories.GetAllAsync();
                var recipeImages = await App._context.RecipeImages.GetAllAsync();
                var languages = await App._context.Languages.GetAllAsync();

                foreach(Recipe recipe in recipes)
                {
                    recipe.Localizations = recipeLocalizations.Where(rl => rl.RecipeID == recipe.ID).ToList();
                    recipe.Categories = recipeCategories.Where(rc => rc.RecipeID == recipe.ID).ToList();
                    recipe.Images = recipeImages.Where(ri => ri.RecipeID == recipe.ID).ToList();
                    var test = recipe.Localizations.Select(rl => rl.LanguageID).ToArray();
                    recipe.Languages = languages.Where(l => test.Contains(l.ID)).ToList();
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
    }
}
