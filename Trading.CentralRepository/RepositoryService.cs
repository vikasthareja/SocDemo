using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trading.CentralRepository;
using Trading.Common;

namespace Trading.CentralRepository
{
    public class RepositoryService : IDisposable
    {
        readonly IMessagingClient messageClient;
        readonly ILogger logger;
        readonly IPersistenceService persistenceService;
        readonly MessageProcessorHelper messageProcessorHelper;
        const string TOPIC = "Trading.TradeProcessing";
        Dictionary<TradeType, ConcurrentQueue<TradeMessage>> queues = new Dictionary<TradeType, ConcurrentQueue<TradeMessage>>();
        List<Task> tasks = new List<Task>();

        public RepositoryService(IMessagingClient messageClient, ILogger logger, IPersistenceService persistenceService)
        {
            this.messageClient = messageClient;
            this.logger = logger;
            this.persistenceService = persistenceService;
            this.messageProcessorHelper = new MessageProcessorHelper(persistenceService);
            logger.Info("Repository service constructed");
        }

        public void Dispose()
        {
            messageClient.UnSubscribe(TOPIC);
        }

        public void Initialize()
        {
            logger.Info("Initiatilizing");
            int sequenceNumbertoBegin;
            try
            {
                sequenceNumbertoBegin = this.GetSequenceNumber();
                InitializeQueues();
                InitializeSubscription();
                logger.Info("Initialization complete");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Exception while initializing the service");

            }
        }

        private void InitializeQueues()
        {
        // Use BlockingCollection in production code.
            Array tradeTypes = Enum.GetValues(typeof(TradeType));
            foreach (TradeType item in tradeTypes)
            {
                ConcurrentQueue<TradeMessage> tradeTypeQueue = new ConcurrentQueue<TradeMessage>();
                queues.Add(item, tradeTypeQueue);
                tasks.Add(Task.Factory.StartNew(() =>
                ProcessMessage(tradeTypeQueue, messageProcessorHelper.GetMessageProcessor(item))));
            }
        }

        /// <summary>
        /// Multithreaded message processor for different trade types. Other options can be discussed.
        /// </summary>
        /// <param name="messageQueue"></param>
        /// <param name="processor"></param>
        private void ProcessMessage(ConcurrentQueue<TradeMessage> messageQueue, IMessageProcessor processor)
        {
            // Retry for multiple times. Then throw a fatal error and block the channel. There can be different versions to continue but tell downstream system
            // to indicate pending erroneous trades.

            // Not implementing the Exception handling, retry and persistence due to time constraint. 
            while (true)
            {
                if (messageQueue.TryDequeue(out TradeMessage msg))
                {
                    logger.Info($"Processing sequence number: {msg.Header.SequenceNumber} and TradeID: {msg.Trade.TradeID} " +
                        $"on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                    processor.ProcessMessage(msg);
                }
            }
        }

        public void MPSMessageHander(MPSMessage message)
        {
            // pre-checks
            // Push message to the queue
            // Optional ACK. This would mean local resilience would be required for processing queue.
            TradeMessageHeader header = message.Header as TradeMessageHeader;
            ITrade trade = message.Payload as ITrade;

            if (header == null)
            {
                logger.Error($"Failed to parse message with header key: {message.Header.MessageKey}");
                return;
            }

            if (trade == null)
            {
                logger.Error($"Failed to parse message with header key: {header.MessageKey} and sequence no: {header.SequenceNumber}");
                return;
            }

            TradeMessage tradeMessage = new TradeMessage() { Header = header, Trade = trade };

            try
            {
                AddMessage(tradeMessage);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, $"Error while adding message with sequence number {tradeMessage.Header.SequenceNumber} to queue. Retrying ...");
                if (!RetryHandler(AddMessage, tradeMessage))
                {
                    logger.Error($"Retries failed for sequence number : {tradeMessage.Header.SequenceNumber}");
                }
            }
        }

        private void AddMessage(TradeMessage message)
        {
            queues[message.Trade.Type].Enqueue(message);
        }

        /// <summary>
        /// Retries for configurable number of time. This should be part of common utility.
        /// </summary>
        /// <param name="retryTask">Method to invoke</param>
        /// <returns>True if invocation is success, else false is returned after maximum retries. </returns>
        private bool RetryHandler(Action<TradeMessage> retryTask, TradeMessage message)
        {
            return true;
        }

        private void InitializeSubscription()
        {
            this.messageClient.Subscribe(TOPIC, MPSMessageHander);
        }

        private int GetSequenceNumber()
        {
            // This method will take care of sequence number resilience (if required with message bus).
            return 1;
        }      
    }
}


public class MessageProcessorHelper
{
    readonly FXProcessor fxProcessor;
    readonly EQProcessor eqProcessor;

    public MessageProcessorHelper(IPersistenceService persistenceService)
    {
        fxProcessor = new FXProcessor(persistenceService);
        eqProcessor = new EQProcessor(persistenceService);
    }

    public IMessageProcessor GetMessageProcessor(TradeType type)
    {
        switch (type)
        {
            case TradeType.EQ:
                return fxProcessor;
            case TradeType.FX:
                return eqProcessor;
            default:
                return eqProcessor; // Just for POC.
        }
    }
}
