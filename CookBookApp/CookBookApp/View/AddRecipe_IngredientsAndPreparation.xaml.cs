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
    public partial class AddRecipe_IngredientsAndPreparation : ContentPage
    {
        public AddRecipe_IngredientsAndPreparation()
        {
            InitializeComponent();

            BindingContext = new AddRecipe_IngredientsAndPreparation();
        }
        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}