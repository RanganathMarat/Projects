using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace First
{
    class Program
    {
        private static async Task<string> Display(string name)
        {
            Log($"Display() 1: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
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
            Log($"Display() 2: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            return "Hello " + name;
        }
        private static async Task<string> Caller()
        {
            Log($"Caller(): Before 0 {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            //await Task.Yield();
            Log($"Caller(): Before 1 {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            string addressDisplay = await Display("Bla").ConfigureAwait(continueOnCapturedContext:false);
            //await Task.Yield();
            Log($"{SynchronizationContext.Current?.ToString()}");
            Log($"Caller(): After {System.Threading.Thread.CurrentThread.ManagedThreadId}");
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
            Log($"FirstCaller(): Before {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            var returnvalue = Caller();
            returnvalue.Wait();            
            //Log(String.Format("FirstCaller(): Return Value {0}", returnvalue));
            Log($"FirstCaller(): After {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            return returnvalue;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            Log($"Main(): Before {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            SynchronizationContext sync = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(sync);
            var returnvalue = FirstCaller();
            //TestDelegates.TestExecute();
            Log($"Main(): Return Value {returnvalue.GetAwaiter().GetResult()}");
            Log($"Main(): After {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            Console.ReadKey();
        }
    }
}
