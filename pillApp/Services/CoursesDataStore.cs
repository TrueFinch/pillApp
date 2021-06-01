using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using pillApp.Models;

namespace pillApp.Services
{
    class CoursesDataStore : IDataStore<Course>
    {
        readonly List<Course> courses;

        public Task<bool> AddItemAsync(Course item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Course item)
        {
            throw new NotImplementedException();
        }
    }
}
