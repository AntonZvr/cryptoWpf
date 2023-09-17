using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class CryptoApiResponse
    {
        public List<CryptoCoin> Data { get; set; }
        public long Timestamp { get; set; }
    }
}
