using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;

namespace Trading.CentralRepository
{
    /// <summary>
    /// In current design. this will interact with Redis distributed cache. Other options can be discussed.
    /// </summary>
    class PersistenceService : IPersistenceService
    {
        public bool AddTrade(ITrade trade)
        {
            throw new NotImplementedException();
        }

        public bool CancelTrade(long tradeId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTrade(ITrade trade)
        {
            throw new NotImplementedException();
        }
    }
}
