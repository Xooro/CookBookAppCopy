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
    public partial class AddRecipe_AllergensAndCategoriesPage : ContentPage
    {
        public AddRecipe_AllergensAndCategoriesPage()
        {
            InitializeComponent();
            BindingContext = new AddRecipe_AlrgnsAndCtgrsVM();
        }
        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void bttnForwardPage_Clicked(object sender, EventArgs e)
        {
        }
    }
}