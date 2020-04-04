using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.CentralRepository
{
    internal class EQProcessor : BaseMessageProcessor
    {
        public EQProcessor(IPersistenceService persistenceService) : 
            base(persistenceService)
        {
        }
    }
}
