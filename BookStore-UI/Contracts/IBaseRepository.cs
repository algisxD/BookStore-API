using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_UI.Contracts
{
    interface IBaseRepository<T> where T : class
    {
        Task<T> Get(string url, int id);
        Task<IList<T>> GetList(string url);
        Task<bool> Create(string url, T obj);
        Task<bool> Update(string url, T obj);
        Task<bool> Delete(string url, int id);
    }
}
