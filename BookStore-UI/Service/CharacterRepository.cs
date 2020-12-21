using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BookStore_UI.Contracts;
using BookStore_UI.Models;

namespace BookStore_UI.Service
{
    public class CharacterRepository : BaseRickAndMortyRepository<Character>, ICharacterRepository
    {
        private readonly IHttpClientFactory _client;
        private readonly ILocalStorageService _localStorage;

        public CharacterRepository(IHttpClientFactory client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }
    }
}
