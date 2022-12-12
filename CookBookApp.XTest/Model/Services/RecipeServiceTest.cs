using CookBookApp.Models;
using CookBookApp.Models.Services;
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


            //Act

            //Assert

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
