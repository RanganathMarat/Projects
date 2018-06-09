using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace First
{
    class TestDelegates
    {
        public static void TestExecute()
        {
            Action<string> actionDelegate = s =>
            {
                Console.WriteLine(s);       
                Console.WriteLine("ActionDelegate(): " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(2000);
            };
            IAsyncResult result = actionDelegate.BeginInvoke("maclean", new AsyncCallback(DummyCallback), null);
            Console.WriteLine("Waiting...." + System.Threading.Thread.CurrentThread.ManagedThreadId);
            actionDelegate.EndInvoke(result);
            Console.WriteLine("Finished....");
            int j = 4;
            for (int i = 0; i < 40000; i++)
            {

                j = j * 23; 
                j = j + 456;
            }
        }

        private static void DummyCallback(IAsyncResult result)
        {
            Console.WriteLine("Inside the DummyCallBack");
            Console.WriteLine("DummyCallback():" + System.Threading.Thread.CurrentThread.ManagedThreadId);

        }

        }
}
