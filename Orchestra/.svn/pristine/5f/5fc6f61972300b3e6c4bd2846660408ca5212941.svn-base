using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Common;
using Common.Messages;
using Common.Messaging;

namespace EquEngine
{
    class Program : IExecuterCallback
    {
        static readonly ManualResetEvent Event = new ManualResetEvent(false);
        private List<Executer> _executers;
        private string _loadedMethod;
        private bool _executionUnderProgress;
        private static Program _instance;
        private IPublish _out;
        private IConsume _in;
        private IPublish _data;

        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Console.CancelKeyPress += Console_CancelKeyPress;

            try
            {
                _instance = new Program
                    {
                        _in = Messaging.Proxy("InMessage").Consumer,
                        _out = Messaging.Proxy("OutMessage").Publisher,
                        _data = Messaging.Proxy("ConsumeData").Publisher
                    };


                _instance._in.AddBinding("Orchestra_Oporational_Out", string.Empty);
                _instance._in.RegisterHandler<Execution.LoadMethod>(_instance.Consume);
                _instance._in.RegisterHandler<Execution.StartExecution>(_instance.Consume);
                _instance._in.RegisterHandler<Execution.StopExecution>(_instance.Consume);
                _instance._in.RegisterHandler<Oporational.Ping>(_instance.Consume);
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

        public static bool HasMainWindow()
        {
            return (Process.GetCurrentProcess().MainWindowHandle != IntPtr.Zero);
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            GraceFullyExit(true);
        }

        private static void GraceFullyExit(bool wait)
        {
            if (wait && HasMainWindow())
            {
                Console.WriteLine("Hit enter to close...");
                Console.ReadLine();
            }

            _instance._in.Dispose();
            _instance._out.Dispose();
            _instance._data.Dispose();
            _instance = null;
            Event.Set();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            GraceFullyExit(true);
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(Execution.LoadMethod message)
        {
            if (message.MethodName.Equals(_loadedMethod))
            {
                Respond(new Execution.LoadResult { MethodName = message.MethodName, Error = true, Result = "Method already Loaded..." });
                return;
            }

            MessgeRecieved(message.GetType() + " " + message.MethodName);

            try
            {
                var instructions = File.ReadAllLines(message.FullName);

                var result = Global.Instructions.Compile(instructions);

                if (string.IsNullOrEmpty(result))
                {
                    var refinedInsts = from inst in instructions
                                       let signal = Global.Instructions.Pars(inst)
                                       where signal != null
                                       select signal;

                    int id = 0;
                    _executers = refinedInsts.Select(signal => new Executer(signal, this) {SignalId  = id++, MethodName = message.MethodName }).ToList();

                    Respond(new Execution.LoadResult { MethodName = message.MethodName, Error = false, Result = "Method Loaded..." });

                    _loadedMethod = message.MethodName;
                }
                else
                {
                    Respond(new Execution.LoadResult { MethodName = message.MethodName, Error = true, Result = result });
                }

            }
            catch (Exception exception)
            {
                _loadedMethod = string.Empty;
                Respond(new Execution.LoadResult { MethodName = message.MethodName, Error = true, Result = exception.Message });
            }
        }

        private void Respond(Execution.LoadResult loadResult)
        {
            _out.Publish(loadResult);
        }

        private void MessgeRecieved(string log)
        {
            Console.WriteLine(log + "...");
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(Execution.StartExecution message)
        {
            if (_executionUnderProgress)
            {
                Respond(new Execution.Ended { MethodName = message.MethodName, Error = true, Message = "Execution under progress..." });
                return;
            }

            MessgeRecieved(message.GetType() + " " + message.MethodName);

            if (message.MethodName == _loadedMethod)
            {
                _executionUnderProgress = true;

                _executers.ForEach(executer => executer.Execute());

                Respond(new Execution.Started { MethodName = message.MethodName });
            }
            else
            {
                Respond(new Execution.Ended { MethodName = message.MethodName, Error = true, Message = "Method not loaded..." });
            }
        }

        private void Respond(Execution.Started started)
        {
            _out.Publish(started);
        }

        private void Respond(Execution.Ended ended)
        {
            _out.Publish(ended);
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(Execution.StopExecution message)
        {
            MessgeRecieved(message.GetType() + " " + message.MethodName);

            if (message.MethodName == _loadedMethod)
            {
                if (_executers == null || _executers.Count == 0)
                {
                    Respond(new Execution.Ended { MethodName = message.MethodName, Error = true, Message = "Method not started..." });
                    return;
                }

                _executers.ForEach(executer => executer.Stop());

                Respond(new Execution.Ended { MethodName = message.MethodName, Error = false, Message = "Method ended..." });
            }
            else
            {
                Respond(new Execution.Ended { MethodName = message.MethodName, Error = true, Message = "Method not loaded..." });
            }
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(Oporational.Shutdown message)
        {
            MessgeRecieved(message.GetType().ToString());

            Event.Set();
        }

        public IPublish DataBus
        {
            get { return _data; }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            if (_executers == null) return;

            Console.WriteLine("Starting the execution" + "...");

            _executers.ForEach(i => i.Start());
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (_executers == null) return;

            Console.WriteLine("Stopping the execution" + "...");
            
            _executionUnderProgress = false;

            _executers.ForEach(i => i.Stop());

            _executers.Clear();
            _executers = null;

            Respond(new Execution.Ended { MethodName = _loadedMethod, Error = false, Message = "Method ended..." });

            _loadedMethod = string.Empty;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Consume(Oporational.Ping message)
        {
            if (message.App == Oporational.AppType.EquEngine)
            {
                Respond(new Oporational.PingResponce { App = message.App });
            }
        }

        private void Respond(Oporational.PingResponce pingResponce)
        {
            _out.Publish(pingResponce);
        }
    }
}
