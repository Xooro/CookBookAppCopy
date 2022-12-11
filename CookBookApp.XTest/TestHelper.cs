using CookBookApp.Data;
using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace CookBookApp.XTest
{
    public class TestHelper
    {
        public static RecipeContext getFilledRecipeContext()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open(); // open connection to use
            var options = new DbContextOptionsBuilder<RecipeContext>()
               .UseSqlite(conn)
               .Options;
            RecipeContext testContext = new RecipeContext(options);

            testContext.Language.AddRange(getTestLanguages());
            testContext.RecipeCategoryNames.AddRange(getTestCategoryNames());
            testContext.Recipe.AddRange(getTestRecipes());
            testContext.RecipeCategories.AddRange(getTestRecipeCategories());
            testContext.RecipeLocalization.AddRange(getTestRecipeLocalizations());
            testContext.RecipeImage.AddRange(getTestRecipeImages());

            testContext.SaveChanges();

            return testContext;
        }

        public static List<Language> getTestLanguages()
        {
            List<Language> languages = new List<Language>()
            {
                new Language()
                {
                    LanguageName = "EN",
                    ImageBytes = ImageHelper.getFlagAsByteArray("EN")
                },
                new Language()
                {
                    LanguageName = "HU",
                    ImageBytes = ImageHelper.getFlagAsByteArray("HU")
                },
                new Language()
                {
                    LanguageName = "DE",
                    ImageBytes = ImageHelper.getFlagAsByteArray("DE")
                }
            };
            return languages;
        }

        public static List<RecipeCategoryNames> getTestCategoryNames()
        {
            List<RecipeCategoryNames> recipeCategoryNames = new List<RecipeCategoryNames>()
                {
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 1,
                        LanguageID = 1,
                        CategoryName = "Soup"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 1,
                        LanguageID = 2,
                        CategoryName = "Leves"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 1,
                        LanguageID = 3,
                        CategoryName = "Suppe"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 2,
                        LanguageID = 1,
                        CategoryName = "Main course"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 2,
                        LanguageID = 2,
                        CategoryName = "Főétel"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 2,
                        LanguageID = 3,
                        CategoryName = "Hauptkurs"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 3,
                        LanguageID = 1,
                        CategoryName = "Dessert"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 3,
                        LanguageID = 2,
                        CategoryName = "Desszert"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 3,
                        LanguageID = 3,
                        CategoryName = "Dessert"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 4,
                        LanguageID = 1,
                        CategoryName = "Festive"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 4,
                        LanguageID = 2,
                        CategoryName = "Ünnepi"
                    },
                    new RecipeCategoryNames()
                    {
                        CategoryNameID = 4,
                        LanguageID = 3,
                        CategoryName = "Festlich"
                    }
                };
            return recipeCategoryNames;
        }

        public static List<Recipe> getTestRecipes()
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
                    Price = 0,
                    Portion = 4,
                    CreationDate = DateTime.Now
                    }
                };
            return recipes;
        }

        public static List<RecipeCategories> getTestRecipeCategories()
        {
            List<RecipeCategories> recipeCategories = new List<RecipeCategories>()
                {
                    new RecipeCategories()
                    {
                        ID = 1,
                        RecipeID = 1,
                        CategoryNameID = 1
                    },
                    new RecipeCategories()
                    {
                        ID = 2,
                        RecipeID = 2,
                        CategoryNameID = 3
                    },
                    new RecipeCategories()
                    {
                        ID = 3,
                        RecipeID = 2,
                        CategoryNameID = 4
                    },
                    new RecipeCategories()
                    {
                        ID = 4,
                        RecipeID = 3,
                        CategoryNameID = 2
                    },
                };
            return recipeCategories;
        }
        
        public static List<RecipeLocalization> getTestRecipeLocalizations()
        {
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
            return recipeLocalizations;
        }

        public static List<RecipeImage> getTestRecipeImages()
        {
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
            return recipeImages;
        }
    }
}
