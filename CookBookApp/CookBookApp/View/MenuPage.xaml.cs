﻿using CookBookApp.ViewModel;
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
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            BindingContext = new MenuViewModel();
            InitializeComponent();
        }

        private void RecipeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecipesPage());
        }
    }
}