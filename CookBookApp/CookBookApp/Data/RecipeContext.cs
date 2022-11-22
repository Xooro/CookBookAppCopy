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
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeCategories> RecipeCategories { get; set; }
        public DbSet<RecipeCategoryNames> RecipeCategoryNames  { get; set; }
        public DbSet<RecipeImage> RecipeImage  { get; set; }
        public DbSet<RecipeLocalization> RecipeLocalization  { get; set; }
        public DbSet<Language> Language { get; set; }

        public RecipeContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "CookBookDB.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
