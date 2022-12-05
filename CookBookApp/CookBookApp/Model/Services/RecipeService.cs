using CookBookApp.Data;
using CookBookApp.Model;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Models.Services
{
    public class RecipeService
    {
        RecipeContext _context;
        public RecipeService()
        {
            _context = new RecipeContext();
        }

        public RecipeService(RecipeContext context)
        {
            _context = context;
        }
        
        //vissza adja a recepteket, amelyek tárolnak minden lokalizációt, de saját lokalizációval nem rendelkezik
        public async Task<List<Recipe>> getRecipesJoinedAsync()
        {
            List<Recipe> recipesResults = new List<Recipe>();
            try
            {
                var recipesTask = _context.Recipe.ToListAsync();
                var recipeLocalizationsTask = _context.RecipeLocalization.ToListAsync();
                var recipeCategoriesTask = _context.RecipeCategories.ToListAsync();
                var recipeImagesTask = _context.RecipeImage.ToListAsync();
                var languagesTask = _context.Language.ToListAsync();

                var recipes = await recipesTask;
                var recipeLocalizations = await recipeLocalizationsTask;
                var recipeCategories = await recipeCategoriesTask;
                var recipeImages = await recipeImagesTask;
                var languages = await languagesTask;

                foreach (Recipe recipe in recipes)
                {
                    recipe.Localizations = recipeLocalizations.Where(rl => rl.RecipeID == recipe.ID).ToList();
                    recipe.Categories = recipeCategories.Where(rc => rc.RecipeID == recipe.ID).ToList();
                    recipe.Images = recipeImages.Where(ri => ri.RecipeID == recipe.ID).ToList();
                    var languageIDs = recipe.Localizations.Select(rl => rl.LanguageID).ToArray();
                    recipe.Languages = languages.Where(l => languageIDs.Contains(l.ID)).ToList();
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

        //Törli a receptet, és a hozzá tartozó elemeket a táblákból
        public async Task<bool> deleteRecipeAsync(Recipe recipeToDelete)
        {
            bool isDeleted = false;
            try
            {
                List<Recipe> recipes = await getRecipesJoinedAsync();
                Recipe recipe = recipes.FirstOrDefault(r => r.ID == recipeToDelete.ID);

                _context.RecipeImage.RemoveRange(recipe.Images);
                _context.RecipeCategories.RemoveRange(recipe.Categories);
                _context.RecipeLocalization.RemoveRange(recipe.Localizations);
                _context.Recipe.Remove(recipe);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }

            return await Task.FromResult(isDeleted);
        }
    }
}
