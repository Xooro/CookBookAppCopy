using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookApp.Models
{
    public class RecipeCategories
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public string Category { get; set; }
    }
}