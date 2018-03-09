using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace _5._1DependencyInversion
{

    public interface ILog
    {
        void WriteLog(string message);
    }

    //[Export(typeof(ILog))]
    //public class Log2 : ILog
    //{
    //    string logFilename;

    //    public string LogFilename
    //    {
    //        get { return logFilename; }
    //        set { logFilename = value; }
    //    }

    //    System.IO.StreamWriter wr;
    //    public Log2()
    //    {
    //        this.logFilename = "logfilename.txt";
    //        wr = new System.IO.StreamWriter(logFilename);

    //    }

    //    public void WriteLog(string message)
    //    {
    //        wr.WriteLine(message);
    //    }

    //    public void CLose()
    //    {
    //        wr.Close();
    //    }
    //}

    [Export(typeof(ILog))]
    public class Log :ILog
    {
        string logFilename;

        public string LogFilename
        {
            get { return logFilename; }
            set { logFilename = value; }
        }

        System.IO.StreamWriter wr;
        public Log()
        {
            this.logFilename = "logfilename.txt";
            wr = new System.IO.StreamWriter(logFilename);

        }

        public void WriteLog(string message)
        {
            wr.WriteLine(message);
        }

        public void CLose()
        {
            wr.Close();
        }
    }

    //Client
    [Export] // Should be marked so for contruction DI
    public class TransactionLayer
    {
        //Dependency
        [Import(typeof(ILog))]
        ILog log;

        [ImportingConstructor]
        public TransactionLayer( ILog logService)
        {
            log = logService;
        }
        public void A()
        {
             log.WriteLog(" A() called");
        }

        public void B()
        {
            log.WriteLog("B() called");
        }

        public void C()
        {
            log.WriteLog("C() called");
        }

    }

     class Program
    {
        static void Main(string[] args)
        {
            // This is composition by hand. This is bad if used at many places.
            //TransactionLayer t = new TransactionLayer( new Log("LogFile.txt");

            // Feed CompositionContainer with a catalog of exports.
            // Different types of catalog supported
            // 1. Directory, 2.Assembly, 3.AggregateCatalog, 4.ConfigurationFile, 5.TypeCatalog
            AssemblyCatalog assemblyCatalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());

            CompositionContainer compositionContainer = new CompositionContainer(assemblyCatalog);
            Lazy<TransactionLayer> lazyWrapper =  compositionContainer.GetExport<TransactionLayer>();
            TransactionLayer t = lazyWrapper.Value;
            t.A();
            t.B();
            t.C();
        }
    }
}
