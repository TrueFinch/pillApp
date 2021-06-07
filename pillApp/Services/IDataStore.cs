using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pillApp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItem(T item);
        Task<bool> UpdateItem(T item);
        Task<bool> DeleteItem(string id);
        Task<T> GetItem(string id);
        Task<IEnumerable<T>> GetItems(bool forceRefresh = false);
    }
}
