using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class MarketDataResponse
    {
        public List<Market> Data { get; set; }
        public long Timestamp { get; set; }
    }
}
