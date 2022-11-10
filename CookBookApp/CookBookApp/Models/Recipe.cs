using SQLite;
using System;
using System.Collections.Generic;

namespace CookBookApp.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public DateTime CreationDate { get; set; }
        public string Author { get; set; }
        public int Difficulty { get; set; }

        [Ignore]
        public virtual List<RecipeCategories> Categories { get; set; }
        [Ignore]
        public virtual List<RecipeImage> Images { get; set; }
    }
}