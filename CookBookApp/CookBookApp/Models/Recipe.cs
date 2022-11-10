using SQLite;
using System;
using System.Collections.Generic;

namespace CookBookApp.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Author { get; set; }
        public DateTime PreparationTime { get; set; }
        public int Difficulty { get; set; }
        public int Price { get; set; }
        public int Portion { get; set; }
        public DateTime CreationDate { get; set; }

        [Ignore]
        public virtual List<RecipeLocalization> Localization { get; set; }
        [Ignore]
        public virtual List<RecipeCategories> Categories { get; set; }
        [Ignore]
        public virtual List<RecipeImage> Images { get; set; }
    }
}