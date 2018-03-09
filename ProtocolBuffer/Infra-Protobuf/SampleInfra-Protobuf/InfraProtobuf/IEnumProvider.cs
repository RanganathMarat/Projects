#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;

namespace Philips.PmsMR.ExternalControl.ModelReflection {
    /// <summary>
    /// Provides enum data for an enum field.
    /// </summary>
    interface IEnumProvider {

        /// <summary>
        /// True if the field is an enum field.
        /// </summary>
        bool IsEnum { get; }

        /// <summary>
        /// Value of the enum field.
        /// </summary>
        int EnumValue { get; }
    }
}
