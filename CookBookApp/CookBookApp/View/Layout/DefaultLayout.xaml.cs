﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBookApp.View.Layout
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DefaultLayout : ContentPage
	{
		public DefaultLayout ()
		{
			InitializeComponent ();
		}
	}
}