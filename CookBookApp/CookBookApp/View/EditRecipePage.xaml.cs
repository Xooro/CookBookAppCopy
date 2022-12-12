using CookBookApp.Models;
using CookBookApp.ViewModel;
using CookBookApp.Views;
using System;
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

        private void btnEditRecipe_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Recipe recipe = (Recipe)button.CommandParameter;

            Navigation.PushAsync(new _AddOrEditRecipePage(recipe));
        }
    }
}