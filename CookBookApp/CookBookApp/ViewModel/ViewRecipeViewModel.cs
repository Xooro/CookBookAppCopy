using CookBookApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookBookApp.ViewModel
{
    public class ViewRecipeViewModel
    {
        public ObservableCollection<string> testList { get; set; }

        public Recipe Recipe { get; set; }

        public ViewRecipeViewModel(Recipe recipe)
        {
            Recipe = recipe;
        }
    }
}
