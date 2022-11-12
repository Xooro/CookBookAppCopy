using CookBookApp.Models;
using CookBookApp.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Helpers
{
    public class ContextHelper
    {
        public static void fillSQLiteWithTestData()
        {
            Task.Run(async () =>
            {
                List<Recipe> recipes = new List<Recipe>()
                {
                    new Recipe()
                    {
                    ID = 1,
                    DefaultLanguageID = 1,
                    Author = "Karoly",
                    PreparationTime = DateTime.Now,
                    Difficulty = 0,
                    Price = 1,
                    Portion = 4,
                    CreationDate = DateTime.Now
                    },
                    new Recipe()
                    {
                    ID = 2,
                    DefaultLanguageID = 3,
                    Author = "Dovi",
                    PreparationTime = DateTime.Now,
                    Difficulty = 1,
                    Price = 2,
                    Portion = 2,
                    CreationDate = DateTime.Now
                    },
                    new Recipe()
                    {
                    ID = 3,
                    DefaultLanguageID = 2,
                    Author = "Name",
                    PreparationTime = DateTime.Now,
                    Difficulty = 2,
                    Price = 3,
                    Portion = 4,
                    CreationDate = DateTime.Now
                    }
                };
                await App._context.Recipes.AddRangeAsync(recipes);

                List<Language> languages = new List<Language>()
                {
                    new Language()
                    {
                        ID = 1,
                        LanguageName = "EN"
                    },
                    new Language()
                    {
                        ID = 2,
                        LanguageName = "HU"
                    },
                    new Language()
                    {
                        ID = 3,
                        LanguageName = "DE"
                    }
                };
                await App._context.Languages.AddRangeAsync(languages);

                List<RecipeCategories> recipeCategories = new List<RecipeCategories>()
                {
                    new RecipeCategories()
                    {
                        ID = 1,
                        RecipeID = 1,
                        LanguageID = 2,
                        CategoryName = "Karácsonyi"
                    },
                    new RecipeCategories()
                    {
                        ID = 2,
                        RecipeID = 1,
                        LanguageID = 2,
                        CategoryName = "Leves"
                    },
                    new RecipeCategories()
                    {
                        ID = 3,
                        RecipeID = 1,
                        LanguageID = 1,
                        CategoryName = "Christmas"
                    },
                    new RecipeCategories()
                    {
                        ID = 4,
                        RecipeID = 1,
                        LanguageID = 1,
                        CategoryName = "Soup"
                    },
                    new RecipeCategories()
                    {
                        ID = 5,
                        RecipeID = 2,
                        LanguageID = 3,
                        CategoryName = "DESSZERT NÉMETÜL"
                    },
                    new RecipeCategories()
                    {
                        ID = 6,
                        RecipeID = 2,
                        LanguageID = 1,
                        CategoryName = "Dessert"
                    },
                    new RecipeCategories()
                    {
                        ID = 7,
                        RecipeID = 3,
                        LanguageID = 2,
                        CategoryName = "Főétel"
                    },
                };
                await App._context.RecipeCategories.AddRangeAsync(recipeCategories);

                List<RecipeLocalization> recipeLocalizations = new List<RecipeLocalization>()
                {
                    new RecipeLocalization()
                    {
                        ID = 1,
                        RecipeID = 1,
                        LanguageID = 1,
                        RecipeName = "Christmass soup",
                        Ingredients = "Somethings",
                        Allergens = "No",
                        Preparation = "Do the following"
                    },
                    new RecipeLocalization()
                    {
                        ID = 2,
                        RecipeID = 1,
                        LanguageID = 2,
                        RecipeName = "Karácsonyi leves",
                        Ingredients = "Valamik",
                        Allergens = "Mincsenek",
                        Preparation = "Csináld a következőt"
                    },
                    new RecipeLocalization()
                    {
                        ID = 3,
                        RecipeID = 2,
                        LanguageID = 3,
                        RecipeName = "DÓCSLEND torta",
                        Ingredients = "német szöveg",
                        Allergens = "nsz megint",
                        Preparation = "preparation németül"
                    },
                    new RecipeLocalization()
                    {
                        ID = 4,
                        RecipeID = 2,
                        LanguageID = 1,
                        RecipeName = "Cake",
                        Ingredients = "Milk Sugar",
                        Allergens = "Milk",
                        Preparation = "BAKE CAKE"
                    },
                    new RecipeLocalization()
                    {
                        ID = 5,
                        RecipeID = 3,
                        LanguageID = 2,
                        RecipeName = "Pörkölt",
                        Ingredients = "Sok hús",
                        Allergens = "A MI?",
                        Preparation = "Csináld meg a pörköltet"
                    }
                };
                await App._context.RecipeLocalizations.AddRangeAsync(recipeLocalizations);

                List<RecipeImage> recipeImages = new List<RecipeImage>()
                {
                    new RecipeImage()
                    {
                        ID = 1,
                        RecipeID = 1,
                        ImageBytes = new byte[10]
                    },
                    new RecipeImage()
                    {
                        ID = 2,
                        RecipeID = 1,
                        ImageBytes = new byte[10]
                    },
                    new RecipeImage()
                    {
                        ID = 3,
                        RecipeID = 2,
                        ImageBytes = new byte[10]
                    },
                    new RecipeImage()
                    {
                        ID = 4,
                        RecipeID = 3,
                        ImageBytes = new byte[10]
                    }
                };
                await App._context.RecipeImages.AddRangeAsync(recipeImages);
            });

        }
    }
}
