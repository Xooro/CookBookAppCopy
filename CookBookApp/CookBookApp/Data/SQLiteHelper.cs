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
        public DBSet<Recipe> Recipes;
        public DBSet<RecipeCategories> RecipeCategories;
        public DBSet<RecipeImage> RecipeImages;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);

            Recipes = new DBSet<Recipe>(db);
            RecipeCategories = new DBSet<RecipeCategories>(db);
            RecipeImages = new DBSet<RecipeImage>(db);
        }
    }
}
