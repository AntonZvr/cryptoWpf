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
using WpfApp.ServicesInterfaces;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for ConverterPage.xaml
    /// </summary>
    public partial class ConverterPage : Page
    {
        public CurrencyConverterViewModel viewModel { get; }

        public ConverterPage()
        {

        }

        public ConverterPage(ICryptoApiService cryptoApiService)
        {
            InitializeComponent();
            viewModel = new CurrencyConverterViewModel(cryptoApiService);
            DataContext = viewModel;
        }

        private void amountText_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.ConvertAmount();
        }

    }
}
