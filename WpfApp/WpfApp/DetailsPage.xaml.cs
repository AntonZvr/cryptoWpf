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
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        public CryptoDetailViewModel viewModel { get; }

        public DetailsPage(CryptoCoin coin)
        {
            InitializeComponent();
            viewModel = new CryptoDetailViewModel { Coin = coin };
            DataContext = viewModel;
        }

        public DetailsPage()
        {
        }
    }
}
