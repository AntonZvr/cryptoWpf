using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class CryptoDetailViewModel : INotifyPropertyChanged
    {
        private CryptoCoin _coin;

        public CryptoCoin Coin
        {
            get => _coin;
            set
            {
                _coin = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
