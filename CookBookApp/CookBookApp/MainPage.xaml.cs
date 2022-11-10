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
        public MainPage()
        {
            InitializeComponent();

            App._context.RecipeImages.getAllAsync();
            //List<Recipe> recipes;
            
            //Task.Run(async () =>
            //{
            //    Recipe recipe = new Recipe();
            //    await App._context.Recipes.AddAsync(recipe);
            //    recipe.ID = 4;
            //    await App._context.Recipes.UpdateAsync(recipe);
            //    recipes = await App._context.Recipes.getAllAsync();
            //    recipes.Remove(recipes.Last());
            //});
        }
    }
}
