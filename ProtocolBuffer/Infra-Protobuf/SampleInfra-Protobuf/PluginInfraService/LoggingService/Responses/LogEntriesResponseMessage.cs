using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInfraServices
{

    /// <summary>
    /// Confirmation message, which indicates that the requested log entries have been written to the log.
    /// </summary>
    public class LogEntriesResponseMessage {
        /// <summary>
        /// Identifies the original request.
        /// </summary>
        public Guid RequestToken;
    }

}
