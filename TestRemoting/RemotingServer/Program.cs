using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Threading.Tasks;

namespace RemotingServer
{
    public class RemoteObject : MarshalByRefObject
    {
        public RemoteObject()
        {
            
        }

        public void Display()
        {
            Console.WriteLine("Calling the remotable object");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IpcChannel channel = new IpcChannel("bla");
            ChannelServices.RegisterChannel(channel, false);

            RemoteObject rmObj = new RemoteObject();
            RemotingServices.Marshal(rmObj, "RemoteObject");
            Console.ReadLine();
        }
    }
}
