using CookBookApp.Helpers;
using CookBookApp.Models;
using CookBookApp.ViewModels.Base;

namespace CookBookApp.ViewModel
{
    public class AddRecipe_NgrdntsAndPrprtnVM : BaseViewModel
    {
        public string UserLanguage { get; set; }
        public string UserName { get; set; }
        public Recipe NewRecipe { get; set; }

        UserSettingsManager userSettingsManager;

        public AddRecipe_NgrdntsAndPrprtnVM(Recipe newRecipe)
        {
            userSettingsManager = new UserSettingsManager();

            NewRecipe = newRecipe;
        }
    }
}
