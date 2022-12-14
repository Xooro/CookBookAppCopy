using CookBookApp.Helpers;
using CookBookApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBookApp.Model
{
    public class Recipe : ICloneable
    {
        public int ID { get; set; }
        public int DefaultLanguageID { get; set; }
        public string Author { get; set; }
        public DateTime PreparationTime { get; set; }
        public int Difficulty { get; set; }
        public int Price { get; set; }
        public int Portion { get; set; }
        public DateTime CreationDate { get; set; }

        //tárolja a recept lokalizált nyelveit
        [NotMapped]
        public List<Language> Languages { get; set; }

        //tárolja a recepthez lekérdezett lokalizált receptjét
        [NotMapped]
        public RecipeLocalization LocalizedRecipe{ get; set; }

        //tárolja a recept lokalizációit
        [NotMapped]
        public List<RecipeLocalization> Localizations { get; set; }

        //tárolja a recept categóriáit
        [NotMapped]
        public List<RecipeCategories> Categories { get; set; }

        //tárolja a recept képeit
        [NotMapped]
        public List<RecipeImage> Images { get; set; }

        [NotMapped]
        public string LocalizedDifficulty
        {
            get
            {
                string[] difficultes = LocalizedConstants.getDifficulties();
                return difficultes[Difficulty];
            }
        }
        [NotMapped]
        public string LocalizedPrice
        {
            get
            {
                string[] prices = LocalizedConstants.getPrices();
                return prices[Price];
            }
        }
        [NotMapped]
        public string LocalizedCategoriesString
        {
            get
            {
                string categories ="";
                foreach (var item in Categories)
                    categories += item.CategoryName + ", ";
                if(categories.Length > 0)
                    categories = categories.Remove(categories.Length - 2);
                return categories;
            }

        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}