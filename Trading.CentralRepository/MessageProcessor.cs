using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;

namespace Trading.CentralRepository
{
    public class MessageProcessor : IMessageProcessor
    {
        public MessageProcessor()
        {
            // Inject logger, Imessagingclient (interface implementation will be mocked in test project)
        }

        internal void Initialize()
        {
            // set up queues and tasks
            // Check for sequence #
            // Subscribe to the bus
        }

        public void ProcessMessage(TradeMessage message)
        {

        }
    }
}
