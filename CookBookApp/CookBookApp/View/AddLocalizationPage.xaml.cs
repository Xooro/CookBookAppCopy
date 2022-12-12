using CookBookApp.Models;
using CookBookApp.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBookApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLocalizationPage : ContentPage
    {
        public AddLocalizationPage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = new AddLocalizationViewModel(recipe);
        }

        private void btnBackPage_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Recipe recipe = (Recipe)button.CommandParameter;

            Navigation.PushAsync(new ViewRecipePage(recipe));
        }
    }
}