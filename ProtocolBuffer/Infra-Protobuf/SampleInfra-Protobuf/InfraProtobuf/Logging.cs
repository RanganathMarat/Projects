using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.PmsMR.Platform.Logging
{

    internal class TraceMessage
    {
        public TraceMessage(string module, string originator)
        {
            this.module = module;
            this.originator = originator;
        }

        public void Info(string message, params object[] args)
        {
            System.Diagnostics.Trace.WriteLine(String.Format(CultureInfo.InvariantCulture, module + "," + originator + ": " + message, args));
        }

        private readonly string module;
        private readonly string originator;
    }

    internal class SystemMessage
    {
        public SystemMessage(string module, string originator)
        {
            this.module = module;
            this.originator = originator;
        }

        public void Info(string message, params object[] args)
        {
            System.Diagnostics.Trace.WriteLine(String.Format(CultureInfo.InvariantCulture, module + "," + originator + ": " + message, args));
        }

        public void Warning(string message, params object[] args)
        {
            System.Diagnostics.Trace.WriteLine(String.Format(CultureInfo.InvariantCulture, module + "," + originator + ": " + message, args));
        }

        public void Error(string message, params object[] args)
        {
            System.Diagnostics.Trace.WriteLine(String.Format(CultureInfo.InvariantCulture, module + "," + originator + ": " + message, args));
        }

        private readonly string module;
        private readonly string originator;
    }

}

