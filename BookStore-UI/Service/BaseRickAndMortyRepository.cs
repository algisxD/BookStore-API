using Blazored.LocalStorage;
using BookStore_UI.Contracts;
using BookStore_UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BookStore_UI.Service
{
    public class BaseRickAndMortyRepository<T> : IBaseRickAndMortyRepository<T> where T : class
    {
        private readonly IHttpClientFactory _client;
        private readonly ILocalStorageService _localStorage;

        public BaseRickAndMortyRepository(IHttpClientFactory client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<Root> GetList(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _client.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Root>(content);
            }

            return null;
        }

        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}
