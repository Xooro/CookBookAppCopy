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

        //tárolja a recept lokalizált nyelveit
        [Ignore]
        public List<Language> Languages { get; set; }

        //tárolja a recepthez lekérdezett lokalizált receptjét
        [Ignore]
        public RecipeLocalization LocalizedRecipe{ get; set; }

        //tárolja a recepthez lekérdezett lokalizált kategóriáit
        [Ignore]
        public List<RecipeCategories> LocalizedCategories { get; set; }

        //tárolja a recept lokalizációit
        [Ignore]
        public List<RecipeLocalization> Localizations { get; set; }

        //tárolja a recept categóriáit
        [Ignore]
        public List<RecipeCategories> Categories { get; set; }

        //tárolja a recept képeit
        [Ignore]
        public List<RecipeImage> Images { get; set; }
        
    }
}