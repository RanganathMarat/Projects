using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInfraServices
{
    /// <summary>
    /// Return the version information to a version information query.
    /// </summary>
    public class QueryVersionResponseMessage {
        /// <summary>
        /// Unique identifier for the original request.
        /// </summary>
        public Guid RequestToken;

        /// <summary>
        /// Product name of the software, e.g., R5.1.7L2
        /// </summary>
        public string Version;

        /// <summary>
        /// MR scanner system type, e.g., WA15 or T30.
        /// </summary>
        public string SystemType;

        /// <summary>
        /// MR scanner product model, e.g., "Achieva dStream" or "Ingenia".
        /// </summary>
        public string ProductModel;
    }

}
