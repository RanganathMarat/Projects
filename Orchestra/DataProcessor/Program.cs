using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Common;
using Common.Messages;
using Common.Messaging;

namespace DataProcessor
{
    class Program
    {
        static readonly ManualResetEvent Event = new ManualResetEvent(false);
        private static Program _instance;
        private IConsume _in;
        private string _currentResultsFiel;

        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Console.CancelKeyPress += Console_CancelKeyPress;

            try
            {
                _instance = new Program
                {
                    _in = Messaging.Proxy("InMessage").Consumer
                };

                _instance._in.AddBinding("Orchestra_EquEngine_Signal", string.Empty);
                _instance._in.AddBinding("Orchestra_Oporational_Out", string.Empty);
                _instance._in.RegisterHandler<Execution.Started>(_instance.Consume);
                _instance._in.RegisterHandler<Execution.DataPoint>(_instance.Consume);
                _instance._in.RegisterHandler<Execution.Ended>(_instance.Consume);
                _instance._in.RegisterHandler<Oporational.Shutdown>(_instance.Consume);
                _instance._in.DeQueue(true);

                Console.WriteLine("Instance initialized");

                Event.WaitOne();

                GraceFullyExit(false);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                GraceFullyExit(true);
            }
        }

        private void Consume(Execution.DataPoint message)
        {
            using (var file = File.AppendText(_currentResultsFiel))
            {
                file.WriteLine(message.ToString());
            }
        }

        private void Consume(Execution.Ended message)
        {
            MessgeRecieved(message.GetType().ToString());

            var directoryName = Path.GetDirectoryName(_currentResultsFiel);

            if (directoryName != null)
            {
                Directory.Move(directoryName, Global.Database.ResultsDb + @"/" + message.MethodName + DateTime.Now.ToFileTime());
            }
        }

        private void Consume(Execution.Started message)
        {
            MessgeRecieved(message.GetType().ToString());

            var resultsPath = Global.Database.ResultsDb + @"/" + message.MethodName + DateTime.Now.ToFileTime() + @"_" +
                              "Temp";

            Directory.CreateDirectory(resultsPath);

            _currentResultsFiel = resultsPath + @"/" + Global.Database.ResultsFile;
        }

        private void Consume(Oporational.Shutdown message)
        {
            MessgeRecieved(message.GetType().ToString());

            Event.Set();
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            GraceFullyExit(true);
        }

        public static bool HasMainWindow()
        {
            return (Process.GetCurrentProcess().MainWindowHandle != IntPtr.Zero);
        }

        private static void GraceFullyExit(bool wait)
        {
            if (wait && HasMainWindow())
            {
                Console.WriteLine("Hit enter to close...");
                Console.ReadLine();
            }

            _instance._in.Dispose();
            _instance = null;
            Event.Set();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            GraceFullyExit(true);
        }

        private static void MessgeRecieved(string log)
        {
            Console.WriteLine(log + "...");
        }
    }
}
