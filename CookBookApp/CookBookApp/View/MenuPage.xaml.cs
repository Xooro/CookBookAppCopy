using CookBookApp.View;
using CookBookApp.ViewModel;
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
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel();
        }

        private void RecipeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecipesPage());
        }
        private void AddRecipeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddRecipe());
        }
    }
}