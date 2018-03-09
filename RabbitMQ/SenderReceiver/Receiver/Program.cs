using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("hello", true, consumer);

                    Console.WriteLine("Waiting to receive messages");

                    while(true)
                    {
                        var eventArgs = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = eventArgs.Body;

                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine("Received the message {0}", message);

                    }
                }
            }
        }
    }
}
