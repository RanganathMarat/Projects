using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInfraServices
{

    /// <summary>
    /// Queries the licenses for provided software options.
    /// </summary>
    public class QueryLicensesRequestMessage {
        /// <summary>
        /// Identifies the request.
        /// </summary>
        public Guid RequestToken;

        /// <summary>
        /// Software options, whose license validity needs to be checked.
        /// </summary>
        /// <remarks>
        /// MR software defines options in the form AWOPT_*, for example, AWOPT_MRCAR and AWOPT_MR_RT.
        /// </remarks>
        public string[] Options;
    }

}
