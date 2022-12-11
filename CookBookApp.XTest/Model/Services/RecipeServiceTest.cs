using CookBookApp.Models;
using CookBookApp.Models.Services;
using System.Collections.Generic;
using Xunit;

namespace CookBookApp.XTest.Model.Services
{
    public class RecipeServiceTest
    {
        [Fact]
        public async void getJoinedRecipesAsync_TestListCount()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledRecipeContext());
            int expectedNumber = TestHelper.getTestRecipes().Count;

            //Act
            List<Recipe> actualList = await recipeService.getJoinedRecipesAsync();

            //Assert
            Assert.Equal(expectedNumber, actualList.Count);
        }
    }
}
