using CookBookApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookBookApp
{
    public partial class MainPage : ContentPage
    {
        //RecipeServices recipeServices;
        //List<Recipe> recipes;
        
        public MainPage()
        {
            InitializeComponent();
            //recipeServices = new RecipeServices();

            //Task.Run(async () =>
            //{
            //    Recipe testRecipe = new Recipe()
            //    {
            //        Author = "Karoly",
            //        PreparationTime = DateTime.Now,
            //        Difficulty = 0,
            //        Price = 1,
            //        Portion = 4,
            //        CreationDate = DateTime.Now
            //    };
            //    await App._context.Recipes.AddAsync(testRecipe);
            //    recipes = await recipeServices.getRecipesByLocatization("HU");
            //});
        }
    }
}
