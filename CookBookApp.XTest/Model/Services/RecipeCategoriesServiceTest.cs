using CookBookApp.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CookBookApp.XTest.Model.Services
{
    public class RecipeCategoriesServiceTest
    {
        [Fact]
        public async void getLocalizedRecipeCategoriesAsync_TestElements()
        {
            //Arrenge
            RecipeCategoriesService categoriesService = new RecipeCategoriesService(TestHelper.getFilledMemoryRecipeContext());

            //Act
            var actualCategoryNames = await categoriesService.getLocalizedRecipeCategoriesAsync(TestHelper.getTestLanguages().First());

            //Assert
            Assert.Equal("Soup", actualCategoryNames[0].CategoryName);
            Assert.Equal("Dessert", actualCategoryNames[2].CategoryName);
        }

        //[Fact]
        //public void _Test()
        //{
        //    //Arrenge
        //    RecipeCategoriesService categoriesService = new RecipeCategoriesService(TestHelper.getFilledMemoryRecipeContext());


        //    //Act


        //    //Assert

        //}
    }
}
