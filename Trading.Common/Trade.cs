using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Common
{
    public class Trade : ITrade
    {
        public long TradeID { get; set; }
        public TradeType Type { get; set; }

        public double Quantity { get; set; }

        public int ClientID { get; set; }

        public int InstrumentUIC { get; set; }
    }
}
