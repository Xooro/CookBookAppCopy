using CookBookApp.Models;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CookBookApp.ViewModel
{
    public class ViewRecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public Image Image { get; set; }

        public ViewRecipeViewModel(Recipe recipe)
        {
            Recipe = recipe;
        }

    }
}
