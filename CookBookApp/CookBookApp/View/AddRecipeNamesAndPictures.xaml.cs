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
    public partial class AddRecipe : ContentPage
    {
        public AddRecipe()
        {
            InitializeComponent();
            BindingContext = new AddRecipeViewModel();
        }

        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void bttnForwardPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AllergensAndCategoriesPage());
        }
    }
}