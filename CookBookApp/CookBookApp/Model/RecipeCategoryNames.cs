using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookApp.Model
{
    public class RecipeCategoryNames
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CategoryNameID { get; set; }
        public int LanguageID { get; set; }
        public string CategoryName { get; set; }
        [Ignore]
        public bool IsChecked { get; set; }
    }
}
