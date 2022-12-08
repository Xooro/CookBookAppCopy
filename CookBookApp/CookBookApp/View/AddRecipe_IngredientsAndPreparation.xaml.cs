using CookBookApp.Models;
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
        public AddRecipe_IngredientsAndPreparation(Recipe newRecipe)
        {
            InitializeComponent();
            BindingContext = new AddRecipe_NgrdntsAndPrprtnVM(newRecipe);
        }
        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}