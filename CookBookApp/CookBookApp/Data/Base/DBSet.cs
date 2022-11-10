using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBookApp.Data.Base
{
    public class DBSet<T> where T : new()
    {
        SQLiteAsyncConnection db;

        public DBSet(SQLiteAsyncConnection db)
        {
            this.db = db;
            db.CreateTableAsync<T>().Wait();
        }

        public Task<int> AddAsync(T modelToAdd)
        {
            return db.InsertAsync(modelToAdd);
        }

        public Task<int> AddRangeAsync(List<T> modelsToAdd)
        {
            return db.InsertAllAsync(modelsToAdd);
        }

        public Task<int> UpdateAsync(T modelToUpdate)
        {
            return db.UpdateAsync(modelToUpdate);
        }

        public Task<int> UpdateRangeAsync(List<T> modelsToUpdate)
        {
            return db.UpdateAllAsync(modelsToUpdate);
        }

        public Task<int> RemoveAsync(T modelToRemove)
        {
            return db.DeleteAsync(modelToRemove);
        }

        public Task<List<T>> getAllAsync()
        {
            return db.Table<T>().ToListAsync();
        }

        public Task<int> clearAsync()
        {
            return db.DeleteAllAsync<T>();
        }
    }
}
