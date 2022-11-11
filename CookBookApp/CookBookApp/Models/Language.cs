using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookApp.Models
{
    public class Language
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //EN,HU,DE, stb....
        public string LanguageName { get; set; }
    }
}
