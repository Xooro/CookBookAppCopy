using Android.App;
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
    public partial class EditRecipePage : ContentPage
    {
        public EditRecipePage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = new EditRecipeViewModel(recipe);
        }
        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void bttnDeleteRecipe_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecipesPage());
        }
    }
}