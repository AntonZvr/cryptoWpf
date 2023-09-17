using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private TopCurrenciesViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();
            _viewModel = new TopCurrenciesViewModel(new CryptoApiService(), coin =>
            {
                var detailPage = new DetailsPage((CryptoCoin)coin, new CryptoApiService());
                this.NavigationService.Navigate(detailPage);
            });
            DataContext = _viewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadDataAsync();
        }

        public void CoinListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedCoin = e.AddedItems[0] as CryptoCoin;
                _viewModel.NavigateToDetail(selectedCoin);
            }
        }
    }
}
