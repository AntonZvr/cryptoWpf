using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class CurrencyRate
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
        public decimal RateUsd { get; set; }
    }

}
