using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInfraServices
{

    /// <summary>
    /// Request to file an audit trail log entry
    /// </summary>
    public class AuditLogEntryRequestMessage
    {

        /// <summary>
        /// Identifies the request.
        /// </summary>
        public Guid RequestToken;

        /// <summary>
        /// Instances-sent log entry.
        /// </summary>
        public InstancesSentEntry InstancesSent;
    }


}
