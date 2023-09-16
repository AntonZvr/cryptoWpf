using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.ServicesInterfaces;

namespace WpfApp.Services
{
    public class CryptoApiService : ICryptoApiService
    {
        private const string BaseUrl = "https://api.coincap.io/v2/";
        private readonly HttpClient _httpClient;

        public CryptoApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<CryptoCoin>> GetTopCoinsAsync(int count)
        {
            var response = await _httpClient.GetAsync($"assets?limit={count}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CryptoApiResponse>(result);
                return data.Data;
            }
            return null; 
        }
    }
}
