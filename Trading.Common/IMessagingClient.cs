using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Common
{
    public interface IMessagingClient : IDisposable
    {
        void Subscribe(string topic, Action<MPSMessage> handler);
        void UnSubscribe(string topic);
    }
}
