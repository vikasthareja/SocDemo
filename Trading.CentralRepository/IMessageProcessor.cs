using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;

namespace Trading.CentralRepository
{
    public interface IMessageProcessor
    {
        // To cater for validations, enrichment and finally persisting the trade.
        void ProcessMessage(TradeMessage message);

    }
}
