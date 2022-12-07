using CookBookApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookApp.View
{
    public class AddRecipeVMHandler
    {
        public AddRecipe_NmsAndPctrsVM AddRecipe_NmsAndPctrsVM { get; set; }
        public AddRecipe_AlrgnsAndCtgrsVM AddRecipe_AlrgnsAndCtgrsVM { get; set; }
        public AddRecipe_NgrdntsAndPrprtnVM AddRecipe_NgrdntsAndPrprtnVM { get; set; }
        
        public AddRecipeVMHandler()
        {
            AddRecipe_NmsAndPctrsVM = new AddRecipe_NmsAndPctrsVM();
            AddRecipe_AlrgnsAndCtgrsVM = new AddRecipe_AlrgnsAndCtgrsVM();
            AddRecipe_NgrdntsAndPrprtnVM = new AddRecipe_NgrdntsAndPrprtnVM();
        }
    }
}
