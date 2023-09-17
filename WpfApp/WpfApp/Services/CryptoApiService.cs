using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void FormatCoinPrice(List<CryptoCoin> coins)
        {
            foreach (var coin in coins)
            {
                if (Decimal.TryParse(coin.PriceUsd, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal coinPriceDecimal))
                {
                    coin.PriceUsd = coinPriceDecimal.ToString("0.0000$");
                }
                else
                {
                    Console.WriteLine($"Failed to parse coin price for coin: {coin.Name}");
                }
            }
        }

        public async Task<MarketDataResponse> GetMarketDataAsync(string coinId)
        {
            var response = await _httpClient.GetAsync($"assets/{coinId}/markets");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MarketDataResponse>(result);
                return data;
            }
            return null;
        }

        public async Task<List<CurrencyRate>> GetCurrencyRatesAsync()
        {
            var response = await _httpClient.GetAsync($"rates");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CurrencyRateResponse>(result);
                return data.Data;
            }
            return null;
        }
    }
}
