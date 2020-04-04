using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;

namespace Trading.CentralRepository
{
    public interface IPersistenceService
    {
        bool AddTrade(ITrade trade);
        bool UpdateTrade(ITrade trade);

        bool CancelTrade(long tradeId);
    }
}
