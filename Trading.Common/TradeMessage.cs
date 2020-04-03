
namespace Trading.Common
{
    public class TradeMessage
    {
        public long SequenceNumber { get; set; }
        public TradeMessageType MessageType { get; set; }
        public ITrade Payload { get; set; }

    }
}
