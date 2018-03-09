using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Philips.PmsMR.Protobuf.DataModel;

namespace PluginInfraServices
{
    /// <summary>
    /// Information about an individual software option.
    /// </summary>
    public class LicenseInformation {
        /// <summary>
        /// Identifies a well-known MR software option.
        /// </summary>
        /// <remarks>
        /// MR software defines options in the form AWOPT_*, for example, AWOPT_MRCAR and AWOPT_MR_RT.
        /// </remarks>
        public string Option;

        /// <summary>
        /// True if the license for the option is available and not expired.
        /// </summary>
        /// <remarks>
        /// When this flag is set, the feature can be used.
        /// </remarks>
        public bool ValidLicenseAvailable;
    }

}
