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
        AddRecipe_AlrgnsAndCtgrsVM viewModel = new AddRecipe_AlrgnsAndCtgrsVM();
        public AddRecipe_AllergensAndCategoriesPage()
        {
            InitializeComponent();
            BindingContext = viewModel;

        }
        private void bttnBackPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void bttnForwardPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRecipe_IngredientsAndPreparation());
            MessagingCenter.Send(viewModel, "NewRecipe", viewModel.NewRecipe);
        }
    }
}