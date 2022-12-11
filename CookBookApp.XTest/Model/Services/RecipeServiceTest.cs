using CookBookApp.Models;
using CookBookApp.Models.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CookBookApp.XTest.Model.Services
{
    public class RecipeServiceTest
    {
        [Fact]
        public void getDefaultEmptyRecipe_TestValid()
        {
            //Arrenge
            RecipeService recipeService = new RecipeService(TestHelper.getFilledRecipeContext());
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
            Assert.Equal(expectedRecipe, actualRecipe);
        }



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

        //[Fact]
        //public void _Test()
        //{
        //    //Arrenge
        //    RecipeService recipeService = new RecipeService(TestHelper.getFilledRecipeContext());

        //    //Act

        //    //Assert
            
        //}


    }
}
