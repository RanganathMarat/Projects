using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInfraServices
{
    /// <summary>
    /// Returns the query information about the sofware options.
    /// </summary>
    public class QueryLicensesResponseMessage {
        /// <summary>
        /// Unique identifier for the original request.
        /// </summary>
        public Guid RequestToken;

        /// <summary>
        /// Information about the license status for software options provided in the original request.
        /// </summary>
        public LicenseInformation[] Licenses;
    }

}
