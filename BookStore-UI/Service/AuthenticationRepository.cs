using Blazored.LocalStorage;
using BookStore_UI.Contracts;
using BookStore_UI.Models;
using BookStore_UI.Providers;
using BookStore_UI.Static;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_UI.Service
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IHttpClientFactory _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationRepository(IHttpClientFactory client,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> Login(LoginModel user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = _client.CreateClient();
            var response = await httpClient.PostAsync(Endpoints.LoginEndPoint, content);

            if(!response.IsSuccessStatusCode)
                return false;

            var content2 = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponse>(content2);

            //Store Token
            await _localStorage.SetItemAsync("authToken", token.Token);

            //Change auth state of app
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }

        public async Task<bool> Register(RegistrationModel user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = _client.CreateClient();
            var response = await httpClient.PostAsync(Endpoints.RegisterEndPoint, content);

            return response.IsSuccessStatusCode;
        }
    }
}
