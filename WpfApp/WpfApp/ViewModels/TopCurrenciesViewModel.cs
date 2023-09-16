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
        private ObservableCollection<CryptoCoin> _allCoins;
        private ObservableCollection<CryptoCoin> _displayedCoins;
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

        public ObservableCollection<CryptoCoin> DisplayedCoins
        {
            get => _displayedCoins;
            set
            {
                _displayedCoins = value;
                OnPropertyChanged(nameof(DisplayedCoins));
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        public ICommand UpdateCommand { get; }
        public ICommand NavigateToDetailCommand { get; }
        public ICommand SearchCommand { get; }

        public TopCurrenciesViewModel(ICryptoApiService cryptoService)
        {
            _cryptoService = cryptoService;

            UpdateCommand = new RelayCommand(_ => LoadDataAsync());
            NavigateToDetailCommand = new RelayCommand(async coin => NavigateToDetail((CryptoCoin)coin));
            SearchCommand = new RelayCommand(_ => Search());
        }

        public Task Search()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                DisplayedCoins = new ObservableCollection<CryptoCoin>(_allCoins);
            }
            else
            {
                DisplayedCoins = new ObservableCollection<CryptoCoin>(_allCoins.Where(c => c.Name.Contains(SearchQuery) || c.Symbol.Contains(SearchQuery)));
            }

            return Task.CompletedTask;
        }

        public async Task LoadDataAsync()
        {
            if (NumberOfCoins > 100)
            {
                MessageBox.Show("Can't display more than 100 currencies");
                return;
            }

            var coins = await _cryptoService.GetTopCoinsAsync(NumberOfCoins);
            _cryptoService.FormatCoinPrice(coins);
            _allCoins = new ObservableCollection<CryptoCoin>(coins);
            DisplayedCoins = new ObservableCollection<CryptoCoin>(_allCoins);
        }

        public void NavigateToDetail(CryptoCoin coin)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var detailPage = new DetailsPage(coin);
            mainWindow.MainFrame.NavigationService.Navigate(detailPage);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
