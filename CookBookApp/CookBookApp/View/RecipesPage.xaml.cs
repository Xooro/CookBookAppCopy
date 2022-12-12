using CookBookApp.Models;
using CookBookApp.View;
using CookBookApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBookApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipesPage : ContentPage
    {
        public RecipesPage()
        {
            InitializeComponent();
            BindingContext = new RecipesViewModel();
        }

        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }
        private void bttnViewRecipe_Clicked(object sender, EventArgs e)
        {

            var button = (Button)sender;
            Recipe newRecipe = (Recipe)button.CommandParameter;

            Navigation.PushAsync(new ViewRecipePage(newRecipe));
        }
    }
}