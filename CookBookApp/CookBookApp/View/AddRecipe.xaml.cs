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
            SizeChanged += addRecipe_SizeChanged;
        }
        private void addRecipe_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}