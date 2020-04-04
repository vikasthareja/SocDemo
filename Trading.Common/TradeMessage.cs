
namespace Trading.Common
{
    public class TradeMessage
    {
        public TradeMessageHeader Header { get; set; }
        public ITrade Trade { get; set; }

    }
}
