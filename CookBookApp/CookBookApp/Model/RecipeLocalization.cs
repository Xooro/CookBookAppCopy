using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookApp.Model
{
    public class RecipeLocalization
    {        
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public int LanguageID { get; set; }
        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string Allergens { get; set; }
        public string Preparation { get; set; }
    }
}
