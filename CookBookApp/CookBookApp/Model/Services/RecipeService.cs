using CookBookApp.Data;
using CookBookApp.Model;
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

        //Visszaadja a receptek listáját a megadott nyelvek alapján
        //üres lita esetén minden recept az alapértelmezett nyelvükkel
        //1 elem alapján a megadott nyelvel rendelkező receptek szerint
        //több elem alapján pedig ha valamelyiket teljesíti
        public async Task<List<Recipe>> getRecipesLocalizedAsync(int[] categoryNameIDs, string[] languages, string search)
        {
            List<Recipe> recipesResults = new List<Recipe>();
            try
            {
                List<Recipe> recipes;

                recipes = await getRecipesByLanguages(languages);
                
                if (search != "")
                {
                    recipes = await getRecipesLocalizedSearched(recipes, search);
                }

                if (categoryNameIDs.Length > 0)
                {
                    recipes = await getRecipesByCategories(recipes, categoryNameIDs);
                }
                recipesResults= recipes;
            }
            catch (Exception ex)
            {
                //TODO: LOGGER CW HELYETT
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(recipesResults);
        }

        private async Task<List<Recipe>> getRecipesByLanguages(string[] languages)
        {
            for (int i = 0; i < languages.Length; ++i)
            {
                languages[i] = languages[i].ToUpper();
            }

            List<Recipe> recipesResults = new List<Recipe>();
            try
            {
                List<Recipe> recipes = new List<Recipe>();

                switch (languages.Length)
                {
                    case 0:
                        recipes = await getRecipesLocalizedNoLanguageAsync();
                        break;
                    case 1:
                        recipes = await getRecipesLocalizedOneLanguageAsync(languages[0]);
                        break;
                    default:
                        recipes = await getRecipesLocalizedMultipleLanguageAsync(languages);
                        break;
                }
                recipesResults = recipes;
            }
            catch
            {
                throw new Exception();
            }
            return await Task.FromResult(recipesResults);
        }

        //a paraméterként megadott recepteket átvizsgálja a categóriák szerint
        private async Task<List<Recipe>> getRecipesByCategories(List<Recipe> recipesToSearch, int[] categoryNameIDs)
        {
            recipesToSearch = recipesToSearch.Where(r => categoryNameIDs.Intersect(r.Categories.Select(c=>c.CategoryNameID).ToArray()).Any()).ToList();
            return await Task.FromResult(recipesToSearch);
        }

        //a paraméterként megadott recepteket átvizsgálja, és visszaadja az elemeket amik megfelelnek a keresésnek
        //keresés az adott helyeken: recept neve, elkészítés, hozzávalók
        private async Task<List<Recipe>> getRecipesLocalizedSearched(List<Recipe> recipesToSearch, string search)
        {
            search = search.ToUpper();
            recipesToSearch = recipesToSearch.Where(r => 
                        (r.LocalizedRecipe.RecipeName.ToUpper().Contains(search)) ||
                        (r.LocalizedRecipe.Preparation.ToUpper().Contains(search)) ||
                        (r.LocalizedRecipe.Ingredients.ToUpper().Contains(search))
                    ).ToList();
            return await Task.FromResult(recipesToSearch);
        }

        //vissza ad minden receptet az alapértelmezett nyelvük alapján
        private async Task<List<Recipe>> getRecipesLocalizedNoLanguageAsync()
        {
            List<Recipe> recipesResults = new List<Recipe>();
            try
            {
                var recipes = await getRecipesJoinedAsync();
                var recipesBuff = new List<Recipe>();
                foreach (Recipe recipe in recipes)
                {
                    Recipe recipeBuff = getLocalizedRecipe(recipe, recipe.DefaultLanguageID);
                    recipesBuff.Add(recipeBuff);
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

        //vissza adja a recepteket, amelyek teljesítik az adott nyelv paraméterét
        private async Task<List<Recipe>> getRecipesLocalizedOneLanguageAsync(string language)
        {
            List<Recipe> recipesResults = new List<Recipe>();
            try
            {
                var recipes = await getRecipesJoinedAsync();
                var recipesBuff = new List<Recipe>();
                var languages = _context.Language.ToList();
                int languageID = languages.FirstOrDefault(l => l.LanguageName == language).ID;

                foreach (Recipe recipe in recipes)
                {
                    if (recipe.Languages.Any(l => l.LanguageName == language))
                    {
                        Recipe recipeBuff = getLocalizedRecipe(recipe, languageID);
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

        //vissza adja a recepteket, amelyek teljesítik az adott nyelveket
        private async Task<List<Recipe>> getRecipesLocalizedMultipleLanguageAsync(string[] languages)
        {
            List<Recipe> recipesResults = new List<Recipe>();
            
            try
            {
                var recipes = await getRecipesLocalizedNoLanguageAsync();
                var recipesBuff = new List<Recipe>();
                foreach (Recipe recipe in recipes)
                {
                    string[] recipeLanguages = recipe.Languages.Select(l=> l.LanguageName).ToArray();
                    if(recipeLanguages.Intersect(languages).Any())
                    {
                        recipesBuff.Add(recipe);
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


        //átalakítja az összes lokalizációt tároló receptet a megadott nyelvet használó receptre
        private Recipe getLocalizedRecipe(Recipe joinedRecipe, int languageID)
        {
            Recipe recipe = joinedRecipe;
            recipe.LocalizedRecipe = recipe.Localizations.FirstOrDefault(l => l.LanguageID == languageID);
            recipe.Categories = getLocalizedRecipeGategories(joinedRecipe, languageID);
            recipe.Localizations = null;

            return recipe;
        }

        //Visszaadja a lokalizált recept kategóriákat
        private List<RecipeCategories> getLocalizedRecipeGategories(Recipe joinedRecipe, int languageID)
        {
            List<RecipeCategoryNames> recipeCategoryNames = new List<RecipeCategoryNames>();
            List<RecipeCategories> recipeCategories = joinedRecipe.Categories;
            Task.Run(async () =>
            {
                recipeCategoryNames = _context.RecipeCategoryNames.ToList();
            }).Wait();
            
            foreach(RecipeCategories category in recipeCategories)
            {
                category.CategoryName = recipeCategoryNames.FirstOrDefault(rcn => 
                    rcn.CategoryNameID == category.CategoryNameID && 
                    rcn.LanguageID == languageID).CategoryName;                                  
            }
            return recipeCategories;
        }

        //vissza adja a recepteket, amelyek tárolnak minden lokalizációt, de saját lokalizációval nem rendelkezik
        public async Task<List<Recipe>> getRecipesJoinedAsync()
        {
            List<Recipe> recipesResults = new List<Recipe>();
            try
            {
                var recipes = _context.Recipe.ToList();
                var recipeLocalizations = _context.RecipeLocalization.ToList();
                var recipeCategories = _context.RecipeCategories.ToList();
                var recipeImages = _context.RecipeImage.ToList();
                var languages = _context.Language.ToList();

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
