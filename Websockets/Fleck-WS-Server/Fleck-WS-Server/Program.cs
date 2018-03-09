using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
namespace Fleck_WS_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fleck Webservice
            var server = new WebSocketServer("ws://127.0.0.1:8181");
            server.Start(socket => 
            {
                socket.OnOpen = () => { Console.WriteLine("Connection opened successfully!!!");
                socket.Send("Connected to the Fleck WebSocket");
                };
                socket.OnClose = () => { Console.WriteLine("Connection closed!!");};
                socket.OnMessage = message => { Console.WriteLine("Received the Message - {0}",message); };
            });
            Console.WriteLine("Server running....");
            Console.ReadLine();

            // NEtMQ WebService
        }
    }
}
