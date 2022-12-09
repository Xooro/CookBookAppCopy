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
    public partial class ViewRecipePage : ContentPage
    {
        public ViewRecipePage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = new ViewRecipeViewModel(recipe);
        }
    }
}