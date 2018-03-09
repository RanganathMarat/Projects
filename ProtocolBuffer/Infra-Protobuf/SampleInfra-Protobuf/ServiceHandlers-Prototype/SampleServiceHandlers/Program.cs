using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Philips.PmsMR.Coreservices.PostProcInfraService
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginInfraServiceHost infraServiceHost = new PluginInfraServiceHost("tcp://127.0.0.1:5656", NetMQ.NetMQContext.Create());
            infraServiceHost.Initialize();
            Console.WriteLine("The current thread ID is - {0}", Thread.CurrentThread.ManagedThreadId);

            //Start another thread and try the CancellationTokenSource
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("Waiting for 5 seconds....");
                Console.ReadLine();
                cancellationTokenSource.Cancel();
                Console.WriteLine("The cancellation token is sent");
            });

            thread.Start();

            infraServiceHost.Start(cancellationTokenSource.Token);

            Console.WriteLine("The service is ended");

            infraServiceHost.Stop();
            infraServiceHost.Dispose();


        }
    }
}
