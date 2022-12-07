using CookBookApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.Models.Services
{
    public class RecipeService
    {
        RecipeContext _context;
        LanguageService languageService;
        public RecipeService()
        {
            _context = new RecipeContext();
            languageService = new LanguageService();
        }

        public RecipeService(RecipeContext context)
        {
            _context = context;
            languageService = new LanguageService(context);
        }

        public Recipe getDefaultEmptyRecipe(string UserName, string UserLanguage)
        {
            Recipe newRecipe = new Recipe();
            Task.Run(async () =>
            {
                newRecipe = new Recipe
                {
                    Author = UserName,
                    CreationDate = DateTime.Now,
                    DefaultLanguageID = (await languageService.getLanguageByName(UserLanguage)).ID,
                    LocalizedRecipe = new RecipeLocalization(),
                    Categories = new List<RecipeCategories>(),
                    Images = new List<RecipeImage>()
                };
            }).Wait();
            
            return newRecipe;
        }
        
        //vissza adja a recepteket, amelyek tárolnak minden lokalizációt, de saját lokalizációval nem rendelkezik
        public async Task<List<Recipe>> getJoinedRecipesAsync()
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
                throw ex;
            }
            return await Task.FromResult(recipesResults);
        }

        public async Task<bool> deleteJoinedRecipeAsync(Recipe recipeToDelete)
        {
            bool isDeleted = false;
            try
            {
                List<Recipe> recipes = await getJoinedRecipesAsync();
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
                throw ex;
            }

            return await Task.FromResult(isDeleted);
        }
    }
}
