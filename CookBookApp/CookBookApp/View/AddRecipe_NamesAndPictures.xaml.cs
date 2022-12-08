using CookBookApp.Models;
using CookBookApp.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBookApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecipe_NamesAndPictures : ContentPage
    {
        public AddRecipe_NamesAndPictures()
        {
            InitializeComponent();
            BindingContext = new AddRecipe_NmsAndPctrsVM();
        }

        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void bttnForwardPage_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Recipe newRecipe = (Recipe)button.CommandParameter;
            
            Navigation.PushAsync(new AddRecipe_AllergensAndCategoriesPage(newRecipe));
        }
    }
}