using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;

namespace Trading.CentralRepository
{
    public class RepositoryService
    {
        readonly IMessageProcessor messageProcessor;

        public RepositoryService(IMessageProcessor messageProcessor, ILogger logger)
        {
            this.messageProcessor = messageProcessor;
            Console.WriteLine("Service Started");
            logger.Info("Repository service started");
        }

        public void Initialize()
        {

        }
    }
}
