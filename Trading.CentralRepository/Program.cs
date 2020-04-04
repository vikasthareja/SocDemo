using System;
using Trading.Common;
using Unity;
using Unity.NLog;

namespace Trading.CentralRepository
{
    // This is POC implementation. Hosting & deployment of real solution would depend on the communications supported, one option
    // would be hosted within windows service.

    // Service is designed to run as Active / Active and scale horizontal, subscribing to messaging on an enterprise bus. 

    // Assumption: 
    // 1. Load balancing would be handled by the message broker solution. Threading model of MPSMessageHandler would depend upon
    // bus and business conditions. This will help to horizontally scale the repository service.
    // 2. Basic exception handling is demonstrated. It has to be much better in real solution.
    // 3. Nlog is logging to file and event viewer. It is assumed that SCOM alerts will be configured against event viewer. Depending
    // upon enterprise Elastic Search + Kabana or Splunk or any other monitoring solution can be used.
    // Multithreading of messages is based on trade type. In a sophisticated solution we can spawn more threads for each trade type
    // maintaining the queue on the basis of account / location. It depends on the scalability needs.

    class Program
    {
        static void Main(string[] args)
        {            
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMessagingClient, MessageClient>();
            container.RegisterType<IPersistenceService, PersistenceService>();
            container.AddNewExtension<NLogExtension>();
            RepositoryService service = container.Resolve<RepositoryService>();
            service.Initialize();
            Console.WriteLine("Processing done. Please press any key to exit ...");
            Console.ReadLine();
        }
    }
}
