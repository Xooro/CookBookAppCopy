using CookBookApp.Helpers;
using CookBookApp.Models;
using CookBookApp.ViewModels.Base;

namespace CookBookApp.ViewModel
{
    public class AddRecipe_NgrdntsAndPrprtnVM : BaseViewModel
    {
        public Recipe NewRecipe { get; set; }

        public AddRecipe_NgrdntsAndPrprtnVM(Recipe newRecipe)
        {
            NewRecipe = newRecipe;
        }
    }
}
