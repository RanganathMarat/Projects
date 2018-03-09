using System;
using System.Messaging;
using System.ServiceProcess;

namespace PurgeAllPrivateQueues_MSMQ
{
    internal class Program
    {
        private static void Main()
        {
            // restart mt subscription service
            ServiceController service = new ServiceController("MassTransit.RuntimeServices", "localhost");
            TimeSpan timeout = TimeSpan.FromMilliseconds(60000);

            if ((service.Status.Equals(ServiceControllerStatus.Stopped)) ||
                                (service.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                DeleteQueues();

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            else
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                DeleteQueues();

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
        }

        private static void DeleteQueues()
        {
            MessageQueue[] msmques = MessageQueue.GetPrivateQueuesByMachine(".");
            foreach (MessageQueue item in msmques)
            {
                MessageQueue.Delete(".\\" + item.QueueName);
            }
        }
    }
}