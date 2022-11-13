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
        public int test { get; set; }
        public RecipesPage()
        {
            test = 1;
            InitializeComponent();
            BindingContext = new RecipesViewModel();
            SizeChanged += MainPageSizeChanged;
        }

        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void MainPageSizeChanged(object sender, EventArgs e)
        {
            var x = recipePage.Width;
            var y = recipePage.Height;

            grid.WidthRequest = x;
            grid.HeightRequest = x;

            gridColumn1.Width = x / 2;
            gridColumn2.Width = x / 2;

            var row1 = gridRow1.Height;

            gridRow2.Height = y * 3 / 12;
            gridRow3.Height = y * 7 / 12;
        }
    }
}