using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCharp
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //TestDelegates.TestExecute();
            //TestNullProperty testNullproperty = new TestNullProperty();
            //testNullproperty.Name = null;
            //TryTraceLog.LogMessages();
            //TryTraceLog.TraceWithTraceSource();
            //TryLinq.Func();
            //TryLinq.TryDelegateInLinq();
            //string[] list = new string[] {"as", "fg", "fhg" };
            //TryWindow.Start();
            //Console.WriteLine(TryMisc.TryDateParse());
            //TryMisc.TryOverride();
            //TryMisc.TryObservableCollection();
            //TryUnity.StartTryUnity();
            //TryUnity.TryUnityInterceptor();           
            //Console.WriteLine(TestConstants.Text1);
            //Console.WriteLine(TestConstants.Text2);
            //var toC = new TryObservableCollection();
            //toC.SubscribeToCollectionChanged();
            //toC.RemoveFromCollection();
            TryUnity.StartTryUnityConstructorPassing();
        }

    }

    //internal static class TestConstants
    //{
    //    public const string Text1 = "bla";
    //    public const string Text2 = "blas";
    //}
}
