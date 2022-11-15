using CookBookApp.ViewModels;
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
    public partial class RecipesPage : ContentPage
    {
        public RecipesPage()
        {
            InitializeComponent();
            BindingContext = new RecipesViewModel();
        }

        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void grdPage_SizeChanged(object sender, EventArgs e)
        {
            var x = recipePage.Width;
            var y = recipePage.Height;

            gridColumn1.Width = x / 2;
            gridColumn2.Width = x / 2;

            gridRow2.Height = y * 3 / 12;
            gridRow3.Height = y * 6 / 12;
        }
    }
}