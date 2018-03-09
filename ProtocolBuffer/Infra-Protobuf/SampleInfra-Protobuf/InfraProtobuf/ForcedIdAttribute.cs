#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;

namespace Philips.PmsMR.Protobuf.DataModel {

    /// <summary>
    /// Forces a given integer id to the attribute target, by-passing name-based hash generation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ForcedIdAttribute : Attribute {

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="overridingId">Integer id value forced on the attribute target</param>
        public ForcedIdAttribute(int overridingId)
        {
            OverridingId = overridingId;
        }

        /// <summary>
        /// Actual integer id value.
        /// </summary>
        public int OverridingId { get; private set; }
    }

}
