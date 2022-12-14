using CookBookApp.Model.Services;
using CookBookApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CookBookApp.XTest.Model.Services
{
    public class LanguageServiceTest
    {
        [Fact]
        public async void getLanguagesAsync_TestLanguages()
        {
            //Arrenge
            LanguageService languageService = new LanguageService(TestHelper.getFilledMemoryRecipeContext());
            var expectedLanguageCount = 3;
            var expectedENLanguageName = "EN";

            //Act
            var actualLanguages = await languageService.getLanguagesAsync();

            //Assert
            Assert.Equal(expectedLanguageCount, actualLanguages.Count);
            Assert.Equal(expectedENLanguageName, actualLanguages.First().LanguageName);
        }

        [Fact]
        public async void getLanguageByIDAsync_TestElement()
        {
            //Arrenge
            LanguageService languageService = new LanguageService(TestHelper.getFilledMemoryRecipeContext());
            var expectedENLanguageName = "EN";

            //Act
            var actualLanguage = await languageService.getLanguageByIDAsync(1);

            //Assert
            Assert.Equal(expectedENLanguageName, actualLanguage.LanguageName);
        }

        [Fact]
        public async void getLanguageByNameAsync_TestElement()
        {
            //Arrenge
            LanguageService languageService = new LanguageService(TestHelper.getFilledMemoryRecipeContext());
            int expectedENLanguageID = 1;

            //Act
            var actualLanguage = await languageService.getLanguageByNameAsync("EN");

            //Assert
            Assert.Equal(expectedENLanguageID, actualLanguage.ID);
        }

        //[Fact]
        //public void _Test()
        //{
        //    //Arrenge
        //    LanguageService languageService = new LanguageService(TestHelper.getFilledMemoryRecipeContext());


        //    //Act


        //    //Assert

        //}
    }
}
