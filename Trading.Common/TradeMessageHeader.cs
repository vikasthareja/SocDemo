using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Common
{
    public class TradeMessageHeader : IMessageHeader
    {
        public long SequenceNumber { get; set; }
        public TradeMessageType MessageType { get; set; }
        public long MessageKey { get; set; }
    }
}
