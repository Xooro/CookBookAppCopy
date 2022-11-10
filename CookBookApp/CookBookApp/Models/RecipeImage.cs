using SQLite;
using System;

namespace CookBookApp.Models
{
    public class RecipeImage
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public byte[] ImageBytes { get; set; }

        [Ignore]
        public string Image
        {
            get
            {
                var base64 = Convert.ToBase64String(ImageBytes);
                return String.Format("data:image/jpg;base64,{0}", base64);
            }
        }
    }
}