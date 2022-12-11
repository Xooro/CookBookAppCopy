using CookBookApp.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.XTest
{
    public class TestHelper
    {
        public static RecipeContext getFilledRecipeConext()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open(); // open connection to use
            var options = new DbContextOptionsBuilder<RecipeContext>()
               .UseSqlite(conn)
               .Options;
            RecipeContext context = new RecipeContext(options);

            return context;
        }
    }
}
