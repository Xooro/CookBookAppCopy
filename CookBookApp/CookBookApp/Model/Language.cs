using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Xamarin.Forms;

namespace CookBookApp.Model
{
    public class Language
    {
        public int ID { get; set; }
        //EN,HU,DE, stb....
        public string LanguageName { get; set; }
        public byte[] ImageBytes { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }
        
        [NotMapped]
        public ImageSource ImageSource
        {
            get
            {
                return ImageSource.FromStream(() => new MemoryStream(ImageBytes));
            }
        }
    }
}
