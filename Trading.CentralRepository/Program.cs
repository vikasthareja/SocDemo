using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;
using Unity;
using Unity.NLog;

namespace Trading.CentralRepository
{
    // This is POC implementation. Design document would contain the scalability details for repository service. 
    // It is designed to run as Active / Active, subscribing to messaging on an enterprise bus. Load balancing would
    // be handled by the message broker solution.

    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMessageProcessor, MessageProcessor>();
            container.AddNewExtension<NLogExtension>();
            RepositoryService service = container.Resolve<RepositoryService>();
            service.Initialize();
        }
    }
}
