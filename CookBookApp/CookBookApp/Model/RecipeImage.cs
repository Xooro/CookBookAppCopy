using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBookApp.Models
{
    public class RecipeImage
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public byte[] ImageBytes { get; set; }

        [NotMapped]
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