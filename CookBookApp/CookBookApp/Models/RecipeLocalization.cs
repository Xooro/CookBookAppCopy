﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookApp.Models
{
    public class RecipeLocalization
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public string Language { get; set; }
        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string Allergens { get; set; }
        public string Preparation { get; set; }
    }
}