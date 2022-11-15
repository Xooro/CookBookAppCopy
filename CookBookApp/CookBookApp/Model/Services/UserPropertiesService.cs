using CookBookApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookApp.Model.Services
{
    public class UserPropertiesService
    {
        public void setUserName(string newUserName)
        {
            App.userProperties.UserName = newUserName;
        }

        public void setLanguage(Language newLanguage)
        {
            App.userProperties.Language = newLanguage.LanguageName;
        }
    }
}
