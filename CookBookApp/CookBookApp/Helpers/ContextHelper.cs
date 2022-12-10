using CookBookApp.Data;
using CookBookApp.Model;
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
        public static void fillContextWithDefaultData()
        {
            RecipeContext _context = new RecipeContext();

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
            _context.Language.AddRange(languages);

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
            _context.RecipeCategoryNames.AddRange(recipeCategoryNames);

            _context.SaveChanges();

        }
        public static void fillSQLiteWithTestData()
        {
            RecipeContext _context = new RecipeContext();

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
                _context.Recipe.AddRange(recipes);

                //List<Language> languages = new List<Language>()
                //{
                //    new Language()
                //    {
                //        ID = 1,
                //        LanguageName = "EN"
                //    },
                //    new Language()
                //    {
                //        ID = 2,
                //        LanguageName = "HU"
                //    },
                //    new Language()
                //    {
                //        ID = 3,
                //        LanguageName = "DE"
                //    }
                //};
                //_context.Language.AddRange(languages);

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
                _context.RecipeCategories.AddRange(recipeCategories);

                //List<RecipeCategoryNames> recipeCategoryNames = new List<RecipeCategoryNames>()
                //{
                //    new RecipeCategoryNames()
                //    {
                //        ID = 1,
                //        CategoryNameID = 1,
                //        LanguageID = 1,
                //        CategoryName = "Soup"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 2,
                //        CategoryNameID = 1,
                //        LanguageID = 2,
                //        CategoryName = "Leves"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 3,
                //        CategoryNameID = 1,
                //        LanguageID = 3,
                //        CategoryName = "Suppe"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 4,
                //        CategoryNameID = 2,
                //        LanguageID = 1,
                //        CategoryName = "Main course"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 5,
                //        CategoryNameID = 2,
                //        LanguageID = 2,
                //        CategoryName = "Főétel"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 6,
                //        CategoryNameID = 2,
                //        LanguageID = 3,
                //        CategoryName = "Hauptkurs"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 7,
                //        CategoryNameID = 3,
                //        LanguageID = 1,
                //        CategoryName = "Dessert"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 8,
                //        CategoryNameID = 3,
                //        LanguageID = 2,
                //        CategoryName = "Desszert"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 9,
                //        CategoryNameID = 3,
                //        LanguageID = 3,
                //        CategoryName = "Dessert"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 10,
                //        CategoryNameID = 4,
                //        LanguageID = 1,
                //        CategoryName = "Festive"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 11,
                //        CategoryNameID = 4,
                //        LanguageID = 2,
                //        CategoryName = "Ünnepi"
                //    },
                //    new RecipeCategoryNames()
                //    {
                //        ID = 12,
                //        CategoryNameID = 4,
                //        LanguageID = 3,
                //        CategoryName = "Festlich"
                //    }
                //};
                //_context.RecipeCategoryNames.AddRange(recipeCategoryNames);

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
                _context.RecipeLocalization.AddRange(recipeLocalizations);

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
                _context.RecipeImage.AddRange(recipeImages);

                await _context.SaveChangesAsync();
            });

        }
    }
}
