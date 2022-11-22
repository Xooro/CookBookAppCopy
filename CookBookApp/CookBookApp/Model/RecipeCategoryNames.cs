using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CookBookApp.Model
{
    public class RecipeCategoryNames
    {
        public int ID { get; set; }
        public int CategoryNameID { get; set; }
        public int LanguageID { get; set; }
        public string CategoryName { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
    }
}
