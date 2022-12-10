using CookBookApp.Helpers;
using CookBookApp.Model;
using CookBookApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace CookBookApp.Data
{
    public class RecipeContext : DbContext
    {
        string databaseName = "CookBookDB.db3";

        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeCategories> RecipeCategories { get; set; }
        public DbSet<RecipeCategoryNames> RecipeCategoryNames  { get; set; }
        public DbSet<RecipeImage> RecipeImage  { get; set; }
        public DbSet<RecipeLocalization> RecipeLocalization  { get; set; }
        public DbSet<Language> Language { get; set; }

        public RecipeContext()
        {
            if (this.Database.EnsureCreated())
                ContextHelper.fillContextWithDefaultData();
        }

        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
            if (this.Database.EnsureCreated())
                ContextHelper.fillContextWithDefaultData();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;
            string dbPath = Path.Combine(Constants.path, databaseName);
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
