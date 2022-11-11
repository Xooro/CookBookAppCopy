using SQLite;

namespace CookBookApp.Models
{
    public class Language
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //EN,HU,DE, stb....
        public string LanguageName { get; set; }

        [Ignore]
        public bool IsChecked { get; set; }
    }
}
