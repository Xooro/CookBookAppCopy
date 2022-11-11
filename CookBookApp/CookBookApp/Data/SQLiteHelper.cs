using CookBookApp.Data.Base;
using CookBookApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public TableSet<Recipe> Recipes;
        public TableSet<RecipeCategories> RecipeCategories;
        public TableSet<RecipeImage> RecipeImages;
        public TableSet<RecipeLocalization> RecipeLocalizations;
        public TableSet<Language> Languages;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);

            Recipes = new TableSet<Recipe>(db);
            RecipeCategories = new TableSet<RecipeCategories>(db);
            RecipeImages = new TableSet<RecipeImage>(db);
            RecipeLocalizations = new TableSet<RecipeLocalization>(db);
            Languages = new TableSet<Language>(db);
        }
    }
}
