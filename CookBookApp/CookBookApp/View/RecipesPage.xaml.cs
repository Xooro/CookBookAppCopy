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
            switch (Device.RuntimePlatform)
            {
                case Device.Android:


                    gridColumn1.Width = x / 4 * 3;
                    gridColumn2.Width = x / 4;


                    gridRow0.Height = y * 1 / 12;
                    gridRow1.Height = y * 2 / 12;
                    gridRow2.Height = y * 2 / 12;
                    gridRow3.Height = y * 5 / 12;
                    gridRow4.Height = y * 2 / 12;


                    break;
                case Device.UWP:

                    gridColumn1.Width = x / 4 * 3;
                    gridColumn2.Width = x / 4;


                    gridRow0.Height = y * 1 / 12;
                    gridRow1.Height = y * 2 / 12;
                    gridRow2.Height = y * 2 / 12;
                    gridRow3.Height = y * 5 / 12;
                    gridRow4.Height = y * 2 / 12;

                    break;
                default:
                    break;
            }

            
        }
    }
}