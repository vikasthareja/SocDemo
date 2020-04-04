using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.CentralRepository
{
    internal class FXProcessor : BaseMessageProcessor
    {
        public FXProcessor(IPersistenceService persistenceService) : 
            base(persistenceService)
        {

        }
    }
}
