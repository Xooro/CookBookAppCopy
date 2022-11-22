using System.ComponentModel.DataAnnotations.Schema;

namespace CookBookApp.Models
{
    public class Language
    {
        public int ID { get; set; }
        //EN,HU,DE, stb....
        public string LanguageName { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }
    }
}
