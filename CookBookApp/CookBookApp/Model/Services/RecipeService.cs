using CookBookApp.Data;
using CookBookApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CookBookApp.Model.Services
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

        //visszaad egy alapértelmezett üres receptet a paraméterként megadott szerzővel és nyelvvel
        public Recipe getDefaultEmptyRecipe(string author, Language language)
        {
            Recipe newRecipe = new Recipe();
            
            newRecipe = new Recipe
            {
                Author = author,
                CreationDate = DateTime.Now,
                DefaultLanguageID = language.ID,
                LocalizedRecipe = new RecipeLocalization
                {
                    LanguageID = language.ID
                },
                Categories = new List<RecipeCategories>(),
                Images = new List<RecipeImage>()
            };

            return newRecipe;
        }


        //egy összekapcsolt, id nélküli receptet feltölt, majd feltölti az adatait az új id-vel
        public async Task<bool> uploadJoinedRecipeWithoutIDAsync(Recipe joinedRecipe)
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

        public Recipe getLocalizedRecipeByRecipe(Recipe recipe, int languageID)
        {
            Recipe joinedRecipe = getJoinedRecipeByRecipe(recipe);
            return getLocalizedRecipeByJoinedRecipe(joinedRecipe, languageID);
        }

        //átalakítja az összes lokalizációt tároló receptet a megadott nyelvet használó receptre
        public Recipe getLocalizedRecipeByJoinedRecipe(Recipe joinedRecipe, int languageID)
        {
            Recipe recipe = joinedRecipe;
            recipe.LocalizedRecipe = recipe.Localizations.FirstOrDefault(l => l.LanguageID == languageID);
            recipe.Categories = getLocalizedRecipeGategories(joinedRecipe, languageID);
            recipe.Localizations = null;

            return recipe;
        }


        //Visszaadja a lokalizált recept kategóriákat
        List<RecipeCategories> getLocalizedRecipeGategories(Recipe joinedRecipe, int languageID)
        {
            List<RecipeCategoryNames> recipeCategoryNames = new List<RecipeCategoryNames>();
            List<RecipeCategories> recipeCategories = joinedRecipe.Categories;
            recipeCategoryNames = _context.RecipeCategoryNames.ToList();

            foreach (RecipeCategories category in recipeCategories)
            {
                category.CategoryName = recipeCategoryNames.FirstOrDefault(rcn =>
                    rcn.CategoryNameID == category.CategoryNameID &&
                    rcn.LanguageID == languageID).CategoryName;
            }
            return recipeCategories;
        }


        //vissza adja a receptet, amely tárol minden lokalizációt, de saját lokalizációval nem rendelkezik
        public Recipe getJoinedRecipeByRecipe(Recipe recipeToBeJoined)
        {
            Recipe joinedRecipe = new Recipe(); 
            Task.Run(async () =>
            {
                List<Recipe> recipes = await getJoinedRecipesAsync();
                joinedRecipe = recipes.FirstOrDefault(r => r.ID == recipeToBeJoined.ID);
            }).Wait();

            return joinedRecipe;
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

        public bool addRecipeLocalization(RecipeLocalization localization)
        {
            bool isLocalizationAdded = false;
            try
            {
                _context.RecipeLocalization.Add(localization);
                _context.SaveChanges();
                isLocalizationAdded = true;
            }
            catch(Exception) { }
            return isLocalizationAdded;
        }

        //Lefrissíti az adatbázist
        public async Task<bool> updateLocalizedRecipeAsync(Recipe localizedRecipeToUpdate)
        {
            bool isUpdated = false;
            try
            {
                await updateRecipeCategoriesInContextAsync(localizedRecipeToUpdate.Categories);
                await updateRecipeImagesInContextAsync(localizedRecipeToUpdate.Images);
                _context.RecipeLocalization.Update(localizedRecipeToUpdate.LocalizedRecipe);
                _context.Recipe.Update(localizedRecipeToUpdate);

                await _context.SaveChangesAsync();
                isUpdated = true;
            }
            catch (Exception) { }

            return await Task.FromResult(isUpdated);
        }

        async Task<bool> updateRecipeCategoriesInContextAsync(List<RecipeCategories> categories)
        {
            bool isUpdated = false;
            try
            {
                int recipeID = categories.First().RecipeID;
                var categoriesInContext = _context.RecipeCategories.ToList().Where(rc => rc.RecipeID == recipeID).ToList();
                
                _context.RemoveRange(categoriesInContext);
                _context.AddRange(categories);  

                isUpdated = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(isUpdated);
        }

        async Task<bool> updateRecipeImagesInContextAsync(List<RecipeImage> images)
        {
            bool isUpdated = false;
            try
            {
                int recipeID = images.First().RecipeID;
                var imagesInContext = _context.RecipeImage.ToList().Where(ri => ri.RecipeID == recipeID);

                _context.RemoveRange(imagesInContext);
                _context.AddRange(images);

                isUpdated = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(isUpdated);
        }

        //törli az adatbázisból a lokalizációt
        public async Task<bool> deleteRecipeLocalizationAsync(RecipeLocalization localizationToDelete)
        {
            bool isDeleted = false;
            try
            {
                _context.RecipeLocalization.Remove(localizationToDelete);
                await _context.SaveChangesAsync();

                isDeleted = true;
            }
            catch (Exception ex) { }

            return await Task.FromResult(isDeleted);
        }

        //törli az adatbázisból a recept nyelv szerinti lokalizációját
        public async Task<bool> deleteRecipeLocalizationAsync(Recipe recipe, Language language)
        {
            bool isDeleted = false;
            try
            {
                Recipe joinedRecipe = getJoinedRecipeByRecipe(recipe);
                RecipeLocalization localizationToDelete = joinedRecipe.Localizations.FirstOrDefault(l => l.LanguageID == language.ID);
                isDeleted = await deleteRecipeLocalizationAsync(localizationToDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(isDeleted);
        }

        //törli az adatbázisból a receptet, és a hozzátartozó incormációkat
        public async Task<bool> deleteRecipeAsync(Recipe recipeToDelete)
        {
            bool isDeleted = false;
            try
            {
                Recipe recipe = getJoinedRecipeByRecipe(recipeToDelete);

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
