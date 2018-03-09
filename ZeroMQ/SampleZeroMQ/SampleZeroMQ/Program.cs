using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using System.Threading;

namespace SampleZeroMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                "1. RequestReply \n" +
                "2. PublishSubscribe \n" +
                "3. PublishSubscribeLoop \n" +
                "4. Reuse the ports");
            Console.WriteLine("Pick a Choice");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case 1: { RequestReply(); break; }
                case 2: { PublishSubsribe(); break;}
                case 3: { PublishSubsribeLoop(); break;}
                case 4: {
                            var trialThread = new Thread(() =>
                            {
                                PublishSubsribeLoop();
                            });
                            trialThread.Start();
                            Thread.Sleep(1000);
                            RequestReplyOnSamePort();
                            break; 
                        }
            }
            
            
            
        }

        private static void PublishSubsribe()
        {
            var connection = "tcp://localhost:44444";
            using ( var context = NetMQ.NetMQContext.Create())
            {
                using (var publishSocket = context.CreatePublisherSocket())
                {
                    publishSocket.Bind(connection);
                    var subscriberThread = new Thread(() => { 
                        using( var subscriberContext = context.CreateSubscriberSocket())
                        {
                            subscriberContext.Connect(connection);
                            subscriberContext.Subscribe("");
                            var receivedString = subscriberContext.ReceiveString();
                            Console.WriteLine("Received from Publisher {0}", receivedString);
                        }
                        });
                    subscriberThread.Start();
                    Thread.Sleep(1000);
                    publishSocket.SendFrame("Hello from Publisher");
                    Console.ReadLine();
                }
            }
        }

         private static void PublishSubsribeLoop()
        {
            var connection = "tcp://localhost:5656";
            using (var context = NetMQContext.Create())
            {
                using (var publishSocket = context.CreatePublisherSocket())
                {
                    publishSocket.Bind(connection);
                    publishSocket.Options.SendHighWatermark = 0;
                    var thread = new Thread(() =>
                    {
                        string topic = "TopicA";
                        Console.WriteLine("Subscriber started for Topic : {0}", topic);

                        using (var subSocket = context.CreateSubscriberSocket())
                        {
                            //subSocket.Options.ReceiveHighWatermark = 0;
                            subSocket.Connect(connection);
                            subSocket.Subscribe("");
                            Console.WriteLine("Subscriber socket connecting...");
                            while (true)
                            {
                                //string messageTopicReceived = subSocket.ReceiveString();
                                string messageReceived = subSocket.ReceiveString();
                                Console.WriteLine(messageReceived);
                            }
                        }
                    });
                    thread.Start();
                    Thread.Sleep(500);
                    Random rand = new Random(50);
                    for (var i = 0; i < 100; i++)
                    {
                        var randomizedTopic = rand.NextDouble();
                        if (randomizedTopic > 0.5)
                        {
                            var msg = "TopicA msg-" + i;
                            Console.WriteLine("Sending message : {0}", msg);
                            publishSocket.SendMore("TopicA").Send(msg);
                        }
                        else
                        {
                            var msg = "TopicB msg-" + i;
                            Console.WriteLine("Sending message : {0}", msg);
                            publishSocket.SendMore("TopicB").Send(msg);
                        }

                        Thread.Sleep(500);
                    }
                    Console.ReadLine();
                }
            }
        }


        private static void RequestReply()
        {
            // Connnection type is in process.
            var connection = "inproc://SampleZeroMQ";
            //var connection = "tcp://127.0.0.1:5656";
            using (var context = NetMQ.NetMQContext.Create())
            {
                using (var responseSocket = context.CreateResponseSocket())
                {
                    responseSocket.Bind(connection);
                    using (var requestSocket = context.CreateRequestSocket())
                    {
                        requestSocket.Connect(connection);

                        requestSocket.Send("Hello From Client");
                        
                        var receivedString = responseSocket.ReceiveString();
                        Console.WriteLine("Received the message from Client - {0}", receivedString);

                        responseSocket.Send("Hello Back from the Server");
                        receivedString = requestSocket.ReceiveString();
                        Console.WriteLine("Received the message back from the Server - {0}", receivedString);
                        Console.ReadLine();
                    }
                }
            }
        }

        private static void RequestReplyOnSamePort()
        {
            // Connnection is on the same port where there is already a publisher.
            var connection = "tcp://127.0.0.1:5656";
            using (var context = NetMQ.NetMQContext.Create())
            {
                try
                {
                    using (var responseSocket = context.CreateResponseSocket())
                    {
                        responseSocket.Bind(connection);
                        using (var requestSocket = context.CreateRequestSocket())
                        {
                            requestSocket.Connect(connection);

                            requestSocket.Send("Hello From Client");

                            var receivedString = responseSocket.ReceiveString();
                            Console.WriteLine("Received the message from Client - {0}", receivedString);

                            responseSocket.Send("Hello Back from the Server");
                            receivedString = requestSocket.ReceiveString();
                            Console.WriteLine("Received the message back from the Server - {0}", receivedString);
                            Console.ReadLine();
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("CANNOT HAVE THE SAME PORT FOR REQUESTREPLY AND PUBLISHSUBSCRIBE");
                }
            }
        }

    }
}
