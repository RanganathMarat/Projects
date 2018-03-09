using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;


namespace Sender
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

                    channel.BasicPublish("", "hello", null, Encoding.UTF8.GetBytes("First Message"));
                    channel.BasicPublish("", "hello", null, Encoding.UTF8.GetBytes("Second Message"));
                    channel.BasicPublish("", "hello", null, Encoding.UTF8.GetBytes("Third Message"));
                    Console.WriteLine(" Sent");
                    Console.Read();
                }
            }


        }
    }
}
