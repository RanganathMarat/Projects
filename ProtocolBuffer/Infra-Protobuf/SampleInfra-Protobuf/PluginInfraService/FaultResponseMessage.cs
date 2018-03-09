using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInfraServices
{
    /// <summary>
    /// Response to an invalid request.
    /// </summary>
    public class FaultResponseMessage {
        /// <summary>
        /// Identifies the invalid request.
        /// </summary>
        public Guid RequestToken;

        /// <summary>
        /// A non-localized failure description for development purposes.
        /// </summary>
        public string Description;
    }

}
