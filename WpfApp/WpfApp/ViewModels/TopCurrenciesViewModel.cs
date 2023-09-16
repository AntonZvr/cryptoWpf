using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp.Models;
using WpfApp.ServicesInterfaces;

namespace WpfApp.ViewModels
{
    public class TopCurrenciesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CryptoCoin> _coins;
        private readonly ICryptoApiService _cryptoService;

        private int _numberOfCoins = 10; // Default value
        public int NumberOfCoins
        {
            get => _numberOfCoins;
            set
            {
                _numberOfCoins = value;
                OnPropertyChanged(nameof(NumberOfCoins));
            }
        }

        public ICommand UpdateCommand { get; }
        public ICommand NavigateToDetailCommand { get; }

        public TopCurrenciesViewModel(ICryptoApiService cryptoService)
        {
            _cryptoService = cryptoService;

            UpdateCommand = new RelayCommand(_ => LoadDataAsync());
            NavigateToDetailCommand = new RelayCommand(async coin => await NavigateToDetail((CryptoCoin)coin));
        }

        public async Task NavigateToDetail(CryptoCoin coin)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var detailPage = new DetailsPage(coin);
            mainWindow.MainFrame.NavigationService.Navigate(detailPage);
        }


        public async Task LoadDataAsync()
            {
            if (NumberOfCoins > 100)
            {
                MessageBox.Show("Can't display more than 100 currencies");
                return;
            }

            var coins = await _cryptoService.GetTopCoinsAsync(NumberOfCoins);
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
            Coins = new ObservableCollection<CryptoCoin>(coins);
            Console.WriteLine(Coins.Count);
        }

        public ObservableCollection<CryptoCoin> Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                OnPropertyChanged(nameof(Coins));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
