using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.ServicesInterfaces;

namespace WpfApp.ViewModels
{
    public class CryptoDetailViewModel : INotifyPropertyChanged
    {
        private CryptoCoin _coin;
        private ICryptoApiService _cryptoApiService;

        public CryptoCoin Coin
        {
            get => _coin;
            set
            {
                _coin = value;
                OnPropertyChanged();
                FetchMarketDataAsync();
            }
        }

        public CryptoDetailViewModel()
        {
           
        }

        public CryptoDetailViewModel(ICryptoApiService cryptoApiService)
        {
            _cryptoApiService = cryptoApiService;
        }

        public async Task FetchMarketDataAsync()
        {
            if (_coin != null)
            {
                var marketData = await _cryptoApiService.GetMarketDataAsync(_coin.Name.ToLower());
                if (marketData != null)
                {
                    _coin.MarketData = marketData.Data;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
