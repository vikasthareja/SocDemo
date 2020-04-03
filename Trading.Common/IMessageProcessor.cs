using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Common
{
    public interface IMessageProcessor
    {
        void ProcessMessage(TradeMessage message);
    }
}
