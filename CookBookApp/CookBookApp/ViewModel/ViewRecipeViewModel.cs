using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookBookApp.ViewModel
{
    public class ViewRecipeViewModel
    {
        public ObservableCollection<string> testList { get; set; } 

        public ViewRecipeViewModel()
        {
            testList = new ObservableCollection<string>() { "test1", "test2", "test3", "test4" };
        }
    }
}
