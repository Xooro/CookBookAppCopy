using CookBookApp.Models;
using CookBookApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBookApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecipe_AllergensAndCategoriesPage : ContentPage
    {
        public AddRecipe_AllergensAndCategoriesPage(Recipe newRecipe)
        {
            InitializeComponent();
            BindingContext = new AddRecipe_AlrgnsAndCtgrsVM(newRecipe);

        }
        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void bttnForwardPage_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Recipe newRecipe = (Recipe)button.CommandParameter;

            Navigation.PushAsync(new AddRecipe_IngredientsAndPreparation(newRecipe));
        }
    }
}