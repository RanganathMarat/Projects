using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInfraServices
{

    /// <summary>
    /// Acknowledging response to the logging of audit trail entries.
    /// </summary>
    public class AuditLogEntryResponseMessage {
        /// <summary>
        /// Identifies the original request.
        /// </summary>
        public Guid RequestToken;
    }
}
