using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInfraServices
{

    /// <summary>
    /// Log the provided entries in one, synchronous operation.
    /// </summary>
    /// <remarks>
    /// Log entries can be accrued to reduce chatter between communicating endpoints.
    /// </remarks>
    public class LogEntriesRequestMessage {
        /// <summary>
        /// Identifies the request.
        /// </summary>
        public Guid RequestToken;

        /// <summary>
        /// Accrued log entries to be logged.
        /// </summary>
        public LogEntry[] EntriesToBeLogged;
    }

}
