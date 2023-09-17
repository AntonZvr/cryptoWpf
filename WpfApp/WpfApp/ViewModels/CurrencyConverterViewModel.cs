using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.ServicesInterfaces;

namespace WpfApp.ViewModels
{
    public class CurrencyConverterViewModel : INotifyPropertyChanged
    {
        private readonly ICryptoApiService _cryptoService;
        private List<CurrencyRate> _rates;
        private CurrencyRate _sourceCurrency;
        private CurrencyRate _targetCurrency;
        private string _amount;
        private string _result;

        public CurrencyConverterViewModel(ICryptoApiService cryptoService)
        {
            _cryptoService = cryptoService;
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CurrencyRate SourceCurrency
        {
            get { return _sourceCurrency; }
            set
            {
                if (value != _sourceCurrency)
                {
                    _sourceCurrency = value;
                    OnPropertyChanged();
                    ConvertAmount();
                }
            }
        }

        public List<CurrencyRate> Rates
        {
            get { return _rates; }
            set
            {
                if (value != _rates)
                {
                    _rates = value;
                    OnPropertyChanged();
                }
            }
        }

        public CurrencyRate TargetCurrency
        {
            get { return _targetCurrency; }
            set
            {
                if (value != _targetCurrency)
                {
                    _targetCurrency = value;
                    OnPropertyChanged();
                    ConvertAmount();
                }
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                if (value != _amount)
                {
                    _amount = value;
                    OnPropertyChanged();
                    ConvertAmount();
                }
            }
        }

        public string Result
        {
            get { return _result; }
            set
            {
                if (value != _result)
                {
                    _result = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task LoadData()
        {
            Rates = await _cryptoService.GetCurrencyRatesAsync();
        }

        private void ConvertAmount()
        {
            var sourceRate = SourceCurrency != null
                             ? Rates.FirstOrDefault(rate => rate.Id == SourceCurrency.Id)?.RateUsd ?? 0
                             : 0;
            var targetRate = TargetCurrency != null
                             ? Rates.FirstOrDefault(rate => rate.Id == TargetCurrency.Id)?.RateUsd ?? 0
                             : 0;

            if (decimal.TryParse(Amount, out var amount))
            {
                var result = amount == 0 || sourceRate == 0 ? 0 : (amount / sourceRate) * targetRate;
                Result = $"{amount} {TargetCurrency?.Id ?? "NULL"} = {Math.Round(result, 2)} {SourceCurrency?.Id ?? "NULL"}";
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
