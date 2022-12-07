using CookBookApp.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBookApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecipe_NamesAndPictures : ContentPage
    {
        AddRecipe_NmsAndPctrsVM viewModel = new AddRecipe_NmsAndPctrsVM();
        public AddRecipe_NamesAndPictures()
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
            await Navigation.PushAsync(new AddRecipe_AllergensAndCategoriesPage());
            MessagingCenter.Send(viewModel, "NewRecipe", viewModel.NewRecipe);
        }
    }
}