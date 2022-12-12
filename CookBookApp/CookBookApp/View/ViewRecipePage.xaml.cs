using CookBookApp.Models;
using CookBookApp.ViewModel;
using CookBookApp.Views;
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
    public partial class ViewRecipePage : ContentPage
    {
        public ViewRecipePage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = new ViewRecipeViewModel(recipe);
        }
        private void bttnEditRecipe_Clicked(object sender, EventArgs e)
        {

            var button = (Button)sender;
            Recipe recipe = (Recipe)button.CommandParameter;

            Navigation.PushAsync(new EditRecipePage(recipe));
        }

        private void bttnLocalizaton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Recipe recipe = (Recipe)button.CommandParameter;

            Navigation.PushAsync(new AddLocalizationPage(recipe));
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecipesPage());
        }
    }
}