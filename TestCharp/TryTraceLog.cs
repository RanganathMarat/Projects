using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestCharp
{
    internal static class TryTraceLog
    {

        public static void TraceWithTraceSource()
        {
                TraceSource traceSource = new TraceSource("MyApplication");
                SourceSwitch sourceSwitch = new SourceSwitch("All");
                traceSource.Switch = sourceSwitch;
                sourceSwitch.Level = SourceLevels.All;
                traceSource.Listeners.Add(new ConsoleTraceListener());
                traceSource.Listeners.Add(new EventLogTraceListener("MyApplication"));
                //traceSource.Listeners.Add(new EventLogTraceListener(new EventLog("Application", "ingbtcpic5nbQ56","MyApplication")));
            
                //traceSource.TraceInformation("trace information 1");

                traceSource.TraceEvent(TraceEventType.Error, 11, "This is a sample trace event");

                Trace.Listeners.Add(new ConsoleTraceListener());
                Trace.TraceInformation("This is information");
                
                
                traceSource.Flush();

        }

        public static void LogMessages()
        {
            using (var textlistener = new TextWriterTraceListener("log.log"))
            {
                Trace.Listeners.Add(textlistener);
                Trace.Listeners.Add(new ConsoleTraceListener());
                System.Diagnostics.TraceSwitch traceSwitch = new TraceSwitch("trace", "All the Trace messages");
                traceSwitch.Level = TraceLevel.Off;
                System.Diagnostics.BooleanSwitch booleanSwitch = new BooleanSwitch("All", "All messages");
                booleanSwitch.Enabled = false;
                Trace.TraceError("This is an error message");
                Trace.TraceInformation("This is information");
                Trace.TraceWarning("This is warning");
                Trace.WriteLine("dump");
                Debug.WriteLine("dump2 debug");
                var tec = new TraceEventCache();
                Console.WriteLine("{0} {1} {2}", tec.Timestamp, tec.ProcessId, tec.ThreadId);
                textlistener.TraceOutputOptions |= TraceOptions.Timestamp;
                textlistener.TraceEvent(new TraceEventCache(), "application", TraceEventType.Error, 101);
                textlistener.Flush();
            }
            
        }

    }
}
