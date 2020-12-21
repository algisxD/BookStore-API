using BookStore_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_UI.Contracts
{
    public interface IBaseRickAndMortyRepository<T> where T : class
    {
        Task<Root> GetList(string url);
    }
}
