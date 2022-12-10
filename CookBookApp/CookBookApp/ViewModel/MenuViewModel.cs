using CookBookApp.Data;
using CookBookApp.Helpers;
using CookBookApp.Models;
using CookBookApp.ViewModels.Base;
using System.Linq;
using Xamarin.Forms;

namespace CookBookApp.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        public Language UserLanguage { get; set; }
        public MenuViewModel()
        {
            RecipeContext _context = new RecipeContext();
            UserLanguage = _context.Language.First();
        }
    }
}
