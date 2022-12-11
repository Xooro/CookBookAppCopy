using CookBookApp.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookApp.Helpers
{
    public class DifficultyHelper
    {
        public static string[] getDifficulties()
        {
            return new string[] {AppResources.CONS_Difficulty_Beginner,
                AppResources.CONS_Difficulty_Intermediate, AppResources.CONS_Difficulty_Advanced};
        }
    }
}
