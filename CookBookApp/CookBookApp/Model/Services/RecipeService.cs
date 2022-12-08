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

        //visszaad egy alapértelmezett üres receptet a paraméterként megadott szerzővel és nyelvvel
        public Recipe getDefaultEmptyRecipe(string author, string language)
        {
            Recipe newRecipe = new Recipe();
            int languageID = 1;
            Task.Run(async () =>
            {
                languageID = (await languageService.getLanguageByName(language)).ID;
            }).Wait();
            
            newRecipe = new Recipe
            {
                Author = author,
                CreationDate = DateTime.Now,
                DefaultLanguageID = languageID,
                LocalizedRecipe = new RecipeLocalization
                {
                    LanguageID = languageID
                },
                Categories = new List<RecipeCategories>(),
                Images = new List<RecipeImage>()
            };

            return newRecipe;
        }


        //egy összekapcsolt, id nélküli receptet feltölt, majd feltölti az adatait az új id-vel
        public async Task<bool> uploadJoinedRecipeWithoutID(Recipe joinedRecipe)
        {
            bool isUploaded = false;
            try
            {
                _context.Recipe.Add(joinedRecipe);
                await _context.SaveChangesAsync();
                int recipeID = _context.Recipe.ToList().Last().ID;

                await updateJoinedRecipeIDs(recipeID, joinedRecipe);
                isUploaded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return await Task.FromResult(isUploaded);
        }

        async Task<bool> updateJoinedRecipeIDs(int id, Recipe joinedRecipe)
        {
            bool isUploaded = false;
            try
            {
                var updatedLocalization = await addIDToRecipeLocalization(id, joinedRecipe.LocalizedRecipe);
                var updatedCategories = await addIDToRecipeCategories(id, joinedRecipe.Categories);
                var updatedImages = await addIDToRecipeImages(id, joinedRecipe.Images);

                _context.RecipeLocalization.Add(updatedLocalization);
                _context.RecipeCategories.AddRange(updatedCategories);
                _context.RecipeImage.AddRange(updatedImages);

                await _context.SaveChangesAsync();

                isUploaded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return await Task.FromResult(isUploaded);
        }

        async Task<RecipeLocalization> addIDToRecipeLocalization(int ID, RecipeLocalization recipeLocalization)
        {
            recipeLocalization.RecipeID = ID;
            return await Task.FromResult(recipeLocalization);
        }

        async Task<List<RecipeCategories>> addIDToRecipeCategories(int ID, List<RecipeCategories> recipeCategories)
        {
            recipeCategories = recipeCategories.Select(rc => { rc.RecipeID = ID; return rc; }).ToList();
            return await Task.FromResult(recipeCategories);
        }

        async Task<List<RecipeImage>> addIDToRecipeImages(int ID, List<RecipeImage> recipeImages)
        {
            recipeImages = recipeImages.Select(ri => { ri.RecipeID = ID; return ri; }).ToList();
            return await Task.FromResult(recipeImages);
        }


        //vissza adja a receptet, amely tárol minden lokalizációt, de saját lokalizációval nem rendelkezik
        public async Task<Recipe> getJoinedRecipeByRecipeAsync(Recipe recipeToBeJoined)
        {
            List<Recipe> recipes = await getJoinedRecipesAsync();
            Recipe joinedRecipe = recipes.FirstOrDefault(r => r.ID == recipeToBeJoined.ID);

            return await Task.FromResult(joinedRecipe);
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


        //törli az adatbázisból a receptet, és a hozzátartozó incormációkat
        public async Task<bool> deleteJoinedRecipeAsync(Recipe recipeToDelete)
        {
            bool isDeleted = false;
            try
            {
                Recipe recipe = await getJoinedRecipeByRecipeAsync(recipeToDelete);

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
