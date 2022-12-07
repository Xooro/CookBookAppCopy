using CookBookApp.ViewModels.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Xamarin.Forms;

namespace CookBookApp.Models
{
    public class RecipeImage
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public byte[] ImageBytes { get; set; }

        [NotMapped]
        public string ImageString
        {
            get
            {
                var base64 = ImageBytes != null ? Convert.ToBase64String(ImageBytes) : "";
                return String.Format("data:image/jpg;base64,{0}", base64);
            }
        }

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