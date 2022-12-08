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
    public partial class AddRecipe_UploadPage : ContentPage
    {
        public AddRecipe_UploadPage(Recipe newRecipe)
        {
            InitializeComponent();
            BindingContext = new AddRecipe_UploadRecipeViewModel(newRecipe);
        }

        private void btnViewRecipe_Clicked(object sender, EventArgs e)
        {
            //ÁKOS OLDALA
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }
    }
}