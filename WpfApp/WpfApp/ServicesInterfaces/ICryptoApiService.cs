using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.ServicesInterfaces
{
    public interface ICryptoApiService
    {
        void FormatCoinPrice(List<CryptoCoin> coins);
        Task<List<CryptoCoin>> GetTopCoinsAsync(int count);
        Task<MarketData> GetMarketDataAsync(string coinId);
    }
}
