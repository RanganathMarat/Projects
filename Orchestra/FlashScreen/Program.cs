using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using System.Windows.Forms;
using Common.Messages;
using Common.Messaging;
using IManageApp;

namespace Orchestra
{
    static class Program
    {
        /// <summary>
        /// The manage apps
        /// </summary>
        private static ManageApplications _manageApps;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _manageApps = new ManageApplications();

            using (var flash = new FlashForm())
            {
                flash.Show();
                Application.DoEvents();

                StartAppProcess(Oporational.AppType.DataProcessor);

                StartAppProcess(Oporational.AppType.EquEngine);

                _manageApps.StartAllApps();

                _manageApps.WaitTillStart();

                flash.Close();
            }

            _manageApps.Wait();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            EndGracefully(e.Exception.Message);
        }

        private static void EndGracefully(string messge)
        {
            if (!string.IsNullOrEmpty(messge))
            {
                MessageBox.Show(messge);
            }

            _manageApps.CloseAll();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            EndGracefully(e.ExceptionObject.ToString());
        }

        /// <summary>
        /// Starts the application process.
        /// </summary>
        /// <param name="app">The application.</param>
        private static void StartAppProcess(Oporational.AppType app)
        {
            Process process = new Process
                {
                    StartInfo = {FileName = app + ".exe", WindowStyle = ProcessWindowStyle.Hidden}
                };
            // Configure the process using the StartInfo properties.
            process.Start();
            //process.WaitForExit();// Waits here for the process to exit.
        }
    }

    /// <summary>
    ///     ManageApplications
    /// </summary>
    public class ManageApplications : MarshalByRefObject, IManageAppCallback, ISponsor 
    {
        private readonly ManualResetEvent _endEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _startEvent = new ManualResetEvent(false);
        readonly ManualResetEvent _waitForReply = new ManualResetEvent(false);

        private IManageApplication _analizeResults;
        private AppDomain _analizeResultsDomain;
        private int _appCount;
        private IManageApplication _executeMethods;
        private AppDomain _executeMethodsDomain;
        private IManageApplication _manageMethods;
        private AppDomain _manageMethodsDomain;
        private readonly IPublish _out;
        private readonly IConsume _in;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageApplications"/> class.
        /// </summary>
        public ManageApplications()
        {
            _out = Messaging.Proxy("OutMessage").Publisher;
            _in = Messaging.Proxy("InMessage").Consumer;
            _in.AddBinding("Orchestra_EquEngine_Out", string.Empty);
            _in.RegisterHandler<Oporational.PingResponce>(Consume);
            _in.RegisterHandler<Oporational.AppStarted>(Consume);
            _in.RegisterHandler<Oporational.AppClosed>(Consume);
            _in.DeQueue(true);
        }

        /// <summary>
        /// Consumes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void Consume(Oporational.AppClosed obj)
        {
            LessOneApp();
        }

        /// <summary>
        /// Consumes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void Consume(Oporational.AppStarted obj)
        {
            PlusOneApp();
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">app</exception>
        public void StartApplication(Oporational.AppType app)
        {
            switch (app)
            {
                case Oporational.AppType.Methods:
                    StartMethods();
                    break;
                case Oporational.AppType.Execute:
                    StartExecute();
                    break;
                case Oporational.AppType.Results:
                    StartAnalize();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("app");
            }
        }

        /// <summary>
        /// Closes all.
        /// </summary>
        public void CloseAll()
        {
            _out.Publish(new Oporational.Shutdown());
            _startEvent.Set();
            _endEvent.Set();
        }

        private bool _result;
        private Oporational.AppType _app;
        private const int AppCount = 3;

        /// <summary>
        /// Pings the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public bool Ping(Oporational.AppType app)
        {
            _result = false;
            _app = app;

            _out.Publish(new Oporational.Ping { App = app });

            _waitForReply.Reset();
            _waitForReply.WaitOne(1000);

            return _result;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="msg">The message.</param>
        private void Consume(Oporational.PingResponce msg)
        {
            _result = msg.App == _app;

            _waitForReply.Set();
        }

        /// <summary>
        /// Applications the closed.
        /// </summary>
        /// <param name="app"></param>
        /// <exception cref="System.ArgumentOutOfRangeException">app</exception>
        public void AppClosed(Oporational.AppType app)
        {
            switch (app)
            {
                case Oporational.AppType.Methods:
                    LessOneApp();
                    _manageMethods = null;
                    break;
                case Oporational.AppType.Execute:
                    LessOneApp();
                    _executeMethods = null;
                    break;
                case Oporational.AppType.Results:
                    LessOneApp();
                    _analizeResults = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("app");
            }
        }

        /// <summary>
        /// Applications the started.
        /// </summary>
        /// <param name="app">The application.</param>
        public void AppStarted(Oporational.AppType app)
        {
            PlusOneApp();
        }

        private void PlusOneApp()
        {
            Interlocked.Increment(ref _appCount);

            if (_appCount == AppCount)
            {
                _startEvent.Set();
            }
        }

        private void LessOneApp()
        {
            Interlocked.Decrement(ref _appCount);

            if (_appCount == 0)
            {
                _endEvent.Set();
            }
        }

        /// <summary>
        ///     Starts all apps.
        /// </summary>
        public void StartAllApps()
        {
            StartMethods();

            StartExecute();

            StartAnalize();
        }

        private void StartAnalize()
        {
            if (_analizeResults != null) return;

            try
            {
                var thread = new Thread(() =>
                {
                    GetAnalizeAppDomain();

                    GetAnalizeApp();

                    _analizeResults.Initialize(this);

                    _analizeResults.Start();
                });

                thread.Name = "_analizeResultsDomain";
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch (Exception error)
            {
                LessOneApp();
                MessageBox.Show(error.Message, @"Failed to create Analize Results");
            }
        }

        private void GetAnalizeApp()
        {
            if (_analizeResults != null) return;

            _analizeResults =
                (IManageApplication)
                _analizeResultsDomain.CreateInstanceAndUnwrap("AnalizeResultsApp", "AnalizeResultsApp.App");
            ((ILease)RemotingServices.GetLifetimeService(this)).Register((ISponsor)_analizeResults);
        }

        private void GetAnalizeAppDomain()
        {
            if (_analizeResultsDomain != null) return;

            _analizeResultsDomain = AppDomain.CreateDomain("_analizeResultsDomain");
            _analizeResultsDomain.SetData("APP_CONFIG_FILE", "AnalizeResultsApp.exe.config");
            _analizeResultsDomain.UnhandledException += AppDomain_UnhandledException;
        }

        private void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            switch (AppDomain.CurrentDomain.FriendlyName)
            {
                case "_analizeResultsDomain":
                    AppDomain.Unload(_analizeResultsDomain);
                    _analizeResultsDomain = null;
                    break;
                case "_executeMethodsDomain":
                    AppDomain.Unload(_executeMethodsDomain);
                    _executeMethodsDomain = null;
                    break;
                case "_manageMethodsDomain":
                    AppDomain.Unload(_manageMethodsDomain);
                    _manageMethodsDomain = null;
                    break;
            }
        }

        private void StartExecute()
        {
            if (_executeMethods != null) return;

            try
            {
                var thread = new Thread(() =>
                    {
                        GetExecuteAppDomain();

                        GetExecutionApp();

                        _executeMethods.Initialize(this);

                        _executeMethods.Start();
                    });

                thread.Name = "_executeMethodsDomain";
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch (Exception error)
            {
                LessOneApp();
                MessageBox.Show(error.Message, @"Failed to create Execute Methods");
            }
        }

        private void GetExecutionApp()
        {
            if (_executeMethods != null) return;

            _executeMethods =
                (IManageApplication)
                _executeMethodsDomain.CreateInstanceAndUnwrap("ExecuteMethodsApp", "ExecuteMethodsApp.App");
            ((ILease)RemotingServices.GetLifetimeService(this)).Register((ISponsor) _executeMethods);
        }

        private void GetExecuteAppDomain()
        {
            if (_executeMethodsDomain != null) return;

            _executeMethodsDomain = AppDomain.CreateDomain("_executeMethodsDomain");
            _executeMethodsDomain.SetData("APP_CONFIG_FILE", "ExecuteMethodsApp.exe.config");
            _executeMethodsDomain.UnhandledException += AppDomain_UnhandledException;
        }

        private void StartMethods()
        {
            if (_manageMethods != null) return;

            try
            {
                var thread = new Thread(() =>
                    {
                        GetMethodsAppDomain();

                        GetMethodsApp();

                        _manageMethods.Initialize(this);

                        _manageMethods.Start();
                    });

                thread.Name = "_manageMethodsDomain";
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch (Exception error)
            {
                LessOneApp();
                MessageBox.Show(error.Message, @"Failed to create Manage Methods");
            }
        }

        private void GetMethodsApp()
        {
            if (_manageMethods != null) return;

            _manageMethods =
                (IManageApplication)
                _manageMethodsDomain.CreateInstanceAndUnwrap("ManageMethodsApp", "ManageMethodsApp.App");
            ((ILease)RemotingServices.GetLifetimeService(this)).Register((ISponsor)_manageMethods);
        }

        private void GetMethodsAppDomain()
        {
            if (_manageMethodsDomain != null) return;

            _manageMethodsDomain = AppDomain.CreateDomain("_manageMethodsDomain");
            _manageMethodsDomain.SetData("APP_CONFIG_FILE", "ManageMethodsApp.exe.config");
            _manageMethodsDomain.UnhandledException += AppDomain_UnhandledException;
        }

        internal void WaitTillStart()
        {
            _startEvent.WaitOne();
        }

        internal void Wait()
        {
            _endEvent.WaitOne();

            _out.Dispose();
            _in.Dispose();
        }

        /// <summary>
        /// Requests a sponsoring client to renew the lease for the specified object.
        /// </summary>
        /// <param name="lease">The lifetime lease of the object that requires lease renewal.</param>
        /// <returns>
        /// The additional lease time for the specified object.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
        ///   </PermissionSet>
        public TimeSpan Renewal(ILease lease)
        {
            Debug.Assert(lease.CurrentState == LeaseState.Active);

            //Renew lease by 5 minutes
            return TimeSpan.FromMinutes(5);
        }
    }
}