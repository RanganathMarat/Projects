using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInfraServices 
{
    /// <summary>
    /// Query version information from MR software.
    /// </summary>
    public class QueryVersionRequestMessage {
        /// <summary>
        /// Identifies the request.
        /// </summary>
        public Guid RequestToken;
    }

}
