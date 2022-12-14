using CookBookApp.Model;
using CookBookApp.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CookBookApp.XTest.Model.Services
{
    public class RecipeServiceTest
    {
        [Fact]
        public void getDefaultEmptyRecipe_TestElement()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());
            string expectedAuthor = "TESTER";

            //Act
            Recipe actualRecipe = recipeService.getDefaultEmptyRecipe(expectedAuthor, TestHelper.getTestLanguages().First());
            string actualAuthor = actualRecipe.Author;

            //Assert
            Assert.Equal(expectedAuthor, actualAuthor);
        }

        [Fact]
        public void getLocalizedRecipeByRecipe_TestElement()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());
            var expectedAllergen = TestHelper.getTestRecipeLocalizations().First().Allergens;

            //Act
            var actualRecipe = recipeService.getLocalizedRecipeByRecipe(TestHelper.getTestRecipes().First(), 
                TestHelper.getTestLanguages().First().ID);
            var actualAllergen = actualRecipe.LocalizedRecipe.Allergens;

            //Assert
            Assert.Equal(expectedAllergen, actualAllergen);
        }

        [Fact]
        public void getLocalizedRecipeByJoinedRecipe_TestElement()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());
            var expectedAllergen = TestHelper.getTestRecipeLocalizations().First().Allergens;

            //Act
            var actualRecipe = recipeService.getLocalizedRecipeByJoinedRecipe(recipeService.getJoinedRecipeByRecipe(TestHelper.getTestRecipes().First()), 
                TestHelper.getTestLanguages().First().ID);
            var actualAllergen = actualRecipe.LocalizedRecipe.Allergens;

            //Assert
            Assert.Equal(expectedAllergen, actualAllergen);
        }

        [Fact]
        public void getDefaultEmptyRecipe_TestValid()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());
            Recipe expectedRecipe = new Recipe
            {
                Author = "TESTER",
                CreationDate = DateTime.Now,
                DefaultLanguageID = 1,
                LocalizedRecipe = new RecipeLocalization
                {
                    LanguageID = 1
                },
                Categories = new List<RecipeCategories>(),
                Images = new List<RecipeImage>()
            };

            //Act
            Recipe actualRecipe = recipeService.getDefaultEmptyRecipe("TESTER", new Language { ID = 1 });
            expectedRecipe.CreationDate = actualRecipe.CreationDate;

            //Assert
            Assert.Equal(expectedRecipe.Author, actualRecipe.Author);
            Assert.Equal(expectedRecipe.DefaultLanguageID, actualRecipe.DefaultLanguageID);
        }


        [Fact]
        public async void getJoinedRecipesAsync_TestListElementsEqualsRecipe()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());
            var expectedUnjoinedRecipe = TestHelper.getTestRecipes();

            //Act
            var actualList = await recipeService.getJoinedRecipesAsync();

            //Assert
            Assert.Equal(expectedUnjoinedRecipe.Count, actualList.Count);
            Assert.Equal(expectedUnjoinedRecipe.First().Author, actualList.First().Author);
            Assert.Equal(expectedUnjoinedRecipe.Last().Difficulty, actualList.Last().Difficulty);
        }

        [Fact]
        public async void getJoinedRecipesAsync_TestListElementsEqualsSubLists()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());
            int expectedFirstElLocalizationNumber = 2;
            int expectedLastElCategoryNumber = 1;
            int expectedMiddleElImagesNumber = 1;

            //Act
            var actualList = await recipeService.getJoinedRecipesAsync();

            //Assert
            Assert.Equal(expectedFirstElLocalizationNumber, actualList.First().Localizations.Count);
            Assert.Equal(expectedLastElCategoryNumber, actualList.Last().Categories.Count);
            Assert.Equal(expectedMiddleElImagesNumber, actualList[1].Images.Count);
        }

        [Fact]
        public void getJoinedRecipesAsync_ThrowException()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getEmptyMemoryRecipeContext());

            //Act
            Func<Task> act = async () => await recipeService.getJoinedRecipesAsync();

            //Assert
            Assert.ThrowsAsync<Exception>(act);
        }

        [Fact]
        public void getJoinedRecipeByRecipe_TestElements()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());
            int expectedLocalizationCounts = 2;

            //Act
            Recipe actualRecipe = TestHelper.getTestRecipes().First();
            actualRecipe = recipeService.getJoinedRecipeByRecipe(actualRecipe);

            //Assert
            Assert.Equal(expectedLocalizationCounts, actualRecipe.Localizations.Count);
        }

        [Fact]
        public void addRecipeLocalization_TestIsAdded()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());

            //Act
            bool isAdded = recipeService.addRecipeLocalization(new RecipeLocalization());

            //Assert
            Assert.True(isAdded);
        }

        [Fact]
        public void addRecipeLocalization_TestIsNotAdded()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());

            //Act
            bool isAdded = recipeService.addRecipeLocalization(new RecipeLocalization { ID = 1});

            //Assert
            Assert.False(isAdded);
        }

        [Fact]
        public async void updateLocalizedRecipeAsync_TestIsUpdated()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());
            string expectedAllergens = "NO ALLERGENS";

            //Act
            Recipe recipe = recipeService.getLocalizedRecipeByRecipe(TestHelper.getTestRecipes().First(), 1);
            recipe.LocalizedRecipe.Allergens = expectedAllergens;
            await recipeService.updateLocalizedRecipeAsync(recipe);
            
            recipe = recipeService.getLocalizedRecipeByRecipe(TestHelper.getTestRecipes().First(), 1);
            string actualAllergens = recipe.LocalizedRecipe.Allergens;

            //Assert
            Assert.Equal(expectedAllergens, actualAllergens);
        }

        [Fact]
        public async void deleteRecipeLocalizationAsync_TestIsDeleted()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());

            //Act
            Recipe recipe = recipeService.getLocalizedRecipeByRecipe(TestHelper.getTestRecipes().First(), 1);
            bool isDeleted = await recipeService.deleteRecipeLocalizationAsync(recipe.LocalizedRecipe);

            //Assert
            Assert.True(isDeleted);
        }

        [Fact]
        public async void deleteRecipeLocalizationAsync_TestIsDeletedByRecipe()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());

            //Act
            Recipe recipe = recipeService.getJoinedRecipeByRecipe(TestHelper.getTestRecipes().First());
            bool isDeleted = await recipeService.deleteRecipeLocalizationAsync(recipe, TestHelper.getTestLanguages().First());

            //Assert
            Assert.True(isDeleted);
        }

        [Fact]
        public async void deleteRecipeAsync_TestIsDeleted()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());

            //Act
            bool isDeleted = await recipeService.deleteRecipeAsync(TestHelper.getTestRecipes().First());

            //Assert
            Assert.True(isDeleted);
        }

        

        //[Fact]
        //public void _Test()
        //{
        //    //Arrenge
        //    RecipeService recipeService = new RecipeService(TestHelper.getFilledMemoryRecipeContext());


        //    //Act


        //    //Assert

        //}


    }
}
