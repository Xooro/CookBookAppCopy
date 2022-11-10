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
            
            //List<Recipe> recipes;
            
            //Task.Run(async () =>
            //{
            //    Recipe recipe = new Recipe()
            //    {
            //        Ingredients = "Só",
            //        Preparation = "Minden is",
            //        CreationDate = DateTime.Now
            //    };
            //    await App.SQLiteDB.Recipes.AddAsync(recipe);
            //    recipe.ID = 4;
            //    recipe.Ingredients = "Minden is";
            //    await App.SQLiteDB.Recipes.UpdateAsync(recipe);
            //    recipes = await App.SQLiteDB.Recipes.getAllAsync();
            //    recipes.Remove(recipes.Last());
            //});
        }
    }
}
