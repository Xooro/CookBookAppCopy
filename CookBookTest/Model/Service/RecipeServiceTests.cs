using CookBookApp.Models;
using CookBookApp.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CookBookTest.Model.Service
{
    public class RecipeServiceTests
    {
        [Fact]
        public void getRecipesJoinedAsync_GetRecipes()
        {
            //Arrange
            List<Recipe> expectedRecipes = new List<Recipe>();

            //Act
            List<Recipe> actualRecipes;
            Task.Run(async () =>
            {
                actualRecipes = await new RecipeService().getRecipesJoinedAsync();
            }).Wait();
            Console.WriteLine("");
        }
    }
}
