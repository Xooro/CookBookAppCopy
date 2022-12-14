using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CookBookApp.Model
{
    public class RecipeCategories
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public int CategoryNameID { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
    }
}