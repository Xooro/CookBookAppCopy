using CookBookApp.Model.Services;
using CookBookApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CookBookApp.XTest.Model.Services
{
    public class RecipeListServiceTest
    {
        [Fact]
        public async void getRecipesLocalizedAsync_TestElementsCount()
        {
            //Arrenge
            RecipesListService recipesListService = new RecipesListService(TestHelper.getFilledMemoryRecipeContext());
            var expectedListCount = 3;

            //Act
            var actualRecipeList = await recipesListService.getRecipesLocalizedAsync(new int[] { }, new int[] { }, "");

            //Assert
            Assert.Equal(expectedListCount, actualRecipeList.Count);
        }

        [Fact]
        public async void getRecipesLocalizedAsync_TestElementsByCategory()
        {
            //Arrenge
            RecipesListService recipesListService = new RecipesListService(TestHelper.getFilledMemoryRecipeContext());
            var expectedListCount = 1;

            //Act
            var actualRecipeList = await recipesListService.getRecipesLocalizedAsync(new int[] { 2}, new int[] { }, "");

            //Assert
            Assert.Equal(expectedListCount, actualRecipeList.Count);
        }

        [Fact]
        public async void getRecipesLocalizedAsync_TestElementsByLanguages()
        {
            //Arrenge
            RecipesListService recipesListService = new RecipesListService(TestHelper.getFilledMemoryRecipeContext());
            var expectedListCount = 2;

            //Act
            var actualRecipeList = await recipesListService.getRecipesLocalizedAsync(new int[] { }, new int[] { 1 }, "");

            //Assert
            Assert.Equal(expectedListCount, actualRecipeList.Count);
        }

        [Fact]
        public async void getRecipesLocalizedAsync_TestElementsBySearch()
        {
            //Arrenge
            RecipesListService recipesListService = new RecipesListService(TestHelper.getFilledMemoryRecipeContext());
            var expectedListCount = 1;

            //Act
            var actualRecipeList = await recipesListService.getRecipesLocalizedAsync(new int[] { }, new int[] { }, "Christ");

            //Assert
            Assert.Equal(expectedListCount, actualRecipeList.Count);
        }

        [Fact]
        public async void getRecipesLocalizedAsync_TestElementsByAll()
        {
            //Arrenge
            RecipesListService recipesListService = new RecipesListService(TestHelper.getFilledMemoryRecipeContext());
            var expectedListCount = 1;

            //Act
            var actualRecipeList = await recipesListService.getRecipesLocalizedAsync(new int[] { }, new int[] { }, "Christ");

            //Assert
            Assert.Equal(expectedListCount, actualRecipeList.Count);
        }

        //[Fact]
        //public void _Test()
        //{
        //    //Arrenge
        //    RecipesListService recipesListService = new RecipesListService(TestHelper.getFilledMemoryRecipeContext());


        //    //Act


        //    //Assert

        //}
    }
}
