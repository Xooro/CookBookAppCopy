using CookBookApp.Data.Base;
using CookBookApp.Model;
using CookBookApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Data
{
    public class RecipeDatabase
    {
        SQLiteAsyncConnection db;

        public EntryTable<Recipe> Recipes;
        public EntryTable<RecipeCategories> RecipeCategories;
        public EntryTable<RecipeCategoryNames> RecipeCategoryNames;
        public EntryTable<RecipeImage> RecipeImages;
        public EntryTable<RecipeLocalization> RecipeLocalizations;
        public EntryTable<Language> Languages;

        public RecipeDatabase(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);

            Recipes = new EntryTable<Recipe>(db);
            RecipeCategories = new EntryTable<RecipeCategories>(db);
            RecipeCategoryNames = new EntryTable<RecipeCategoryNames>(db);
            RecipeImages = new EntryTable<RecipeImage>(db);
            RecipeLocalizations = new EntryTable<RecipeLocalization>(db);
            Languages = new EntryTable<Language>(db);
        }
    }
}
