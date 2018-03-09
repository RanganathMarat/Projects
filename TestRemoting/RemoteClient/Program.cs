using System;
using System.Threading.Tasks;
using RemotingServer;
using System.Management;
using System.Security.Permissions;

namespace RemoteClient
{
    class Program
    {
        private static RemoteObject _remObj;

        [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        static void Main(string[] args)
        {
            try
            {
                _remObj = Activator.GetObject(typeof(RemoteObject), "ipc://bla/RemoteObject") as RemoteObject;
                _remObj?.Display();
            }
            catch (System.Runtime.Remoting.RemotingException e)
            {
                _remObj = null;
                //Console.WriteLine(e);
                var returnTask = StartWatching();
                returnTask.Wait();
                //_remObj?.Display();
            }
            Console.ReadLine();
        }

        private static Task StartWatching()
        {
            return Task.Run((Action) WaitForProcess);
        }


        private static void WaitForProcess()
        {
            var startWatch = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            startWatch.EventArrived += (sender, e) =>
            {
                Console.WriteLine("Event received on client" );
                Console.WriteLine(e.NewEvent.ToString());
                try
                {
                    _remObj = Activator.GetObject(typeof(RemoteObject), "ipc://bla/RemoteObject") as RemoteObject;
                    _remObj?.Display();
                }
                catch (System.Runtime.Remoting.RemotingException ee)
                {
                }
            };
                startWatch.Start();
                startWatch.WaitForNextEvent();
            }
        }
    }
