using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;

namespace Trading.CentralRepository
{
    public class MessageClient : IMessagingClient
    {
        public MessageClient()
        {
            // Inject Logger
            Initialize();
        }

        internal void Initialize()
        {
           
            // Initialize connections.
        }

        public void Subscribe(string topic, Action<MPSMessage> handler)
        {
            // Mock testing of subscription.
            for (int i = 1; i <= 1500; i++)
            {
                Trade T = new Trade();
                if (i % 3 == 1)
                {
                    T.Type = TradeType.EQ;
                }
                else if (i % 3 == 2)
                {
                    T.Type = TradeType.FX;
                }
                else
                {
                    T.Type = TradeType.Opt;
                }
                T.TradeID = i;

                TradeMessageHeader header = new TradeMessageHeader() { MessageKey = i, MessageType = TradeMessageType.Added, SequenceNumber = i };

                MPSMessage message = new MPSMessage() { Header = header, Payload = T };
                handler.Invoke(message);
            }
        }

        public void UnSubscribe(string topic)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
