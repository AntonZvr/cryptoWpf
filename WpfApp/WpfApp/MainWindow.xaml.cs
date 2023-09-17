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
using WpfApp.Services;
using WpfApp.ServicesInterfaces;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICryptoApiService _cryptoApiService;
        public Frame MainFrame => Main;
        public MainWindow()
        {
            var topCurrenciesViewModel = new TopCurrenciesViewModel(
            new CryptoApiService(),
            coin =>
            {
                var detailPage = new DetailsPage((Models.CryptoCoin)coin, new CryptoApiService());
                Main.Navigate(detailPage);
            });
            InitializeComponent();
            _cryptoApiService = new CryptoApiService();
        }

        private ConverterPage CreateConverterPage()
        {
            return new ConverterPage(_cryptoApiService);
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage();
        }

        private void ButtonClick3(object sender, RoutedEventArgs e)
        {
            Main.Content = CreateConverterPage();
        }
    }
}
