using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;

namespace Trading.CentralRepository
{
    internal abstract class BaseMessageProcessor : IMessageProcessor
    {
        protected readonly IPersistenceService persistenceService;

        public BaseMessageProcessor(IPersistenceService persistenceService)
        {
            this.persistenceService = persistenceService;
        }

        public void ProcessMessage(TradeMessage message)
        {
            // Base processing
        }
       
    }
}
