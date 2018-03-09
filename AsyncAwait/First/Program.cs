using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First
{
    class Program
    {
        private static async Task<string> Display(string name)
        {
            Log(String.Format("Display() 1: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            await Task.Delay(3000);

            // A thread sleep is not an awaitable method. Adding a sleep makes the whole aync-await irrelevant.
            //System.Threading.Thread.Sleep(5000);
            //Log(String.Format("Display() 1: TaskScheduler.Default.Id {0}", TaskScheduler.Default.Id));
            // This behavior is similar to Task.Delay(). Even in Task.Delay(), a thread 
            //await Task.Run(() =>
            //{
            //    Log(String.Format("Task.RUN(): {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            //    Log(String.Format("Task.RUN(): TaskScheduler.Current.Id {0}", TaskScheduler.Current.Id));
            //    System.Threading.Thread.Sleep(3000);
            //    Log("Task Complete");
            //});
            //Log(String.Format("Display() 2: TaskScheduler.Current.Id {0}", TaskScheduler.Current.Id));
            Log(String.Format("Display() 2: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            return "Hello " + name;
        }
        private static async Task<string> Caller()
        {
            Log(String.Format("Caller(): Before {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            string addressDisplay = await Display("Bla");
            Log(String.Format("Caller(): After {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            //string addressDisplay = await address;
            //Log(String.Format("Caller(2): {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            Log("Exiting Caller() " + addressDisplay);
            return addressDisplay;
        }
        private static void Log(string logM)
        {
            Console.WriteLine(DateTime.Now + " " + logM);
        }

        private static Task<string> FirstCaller()
        {
            Log(String.Format("FirstCaller(): Before {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            var returnvalue = Caller();
            returnvalue.Wait();
            //Log(String.Format("FirstCaller(): Return Value {0}", returnvalue));
            Log(String.Format("FirstCaller(): After {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            return returnvalue;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            Log(String.Format("Main(): Before {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            var returnvalue = FirstCaller();
            //Log(String.Format("Main(): Return Value {0}", returnvalue.GetAwaiter().GetResult()));
            Log(String.Format("Main(): After {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
            Console.ReadKey();
        }
    }
}
