using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Common
{
   public enum TradeMessageType
    {
        Added = 1,
        Amended,
        Cancelled // only intra-day.
    }
}
