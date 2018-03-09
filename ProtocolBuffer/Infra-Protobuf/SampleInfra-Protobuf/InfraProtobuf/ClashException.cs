#region  Copyright 2013 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Philips.PmsMR.Protobuf.ModelReflection;

namespace Philips.PmsMR.Protobuf.ModelReflection
{
    /// <summary>
    /// Id clash exception in data model.
    /// </summary>
    [Serializable]
    public class ClashException : ModelReflectionException
    {
        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="newFullname"></param>
        /// <param name="oldFullname"></param>
        /// <param name="conflictingId"></param>
        public ClashException(string newFullname, string oldFullname, Int32 conflictingId) : this(newFullname, oldFullname, conflictingId, null) { }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="newFullname"></param>
        /// <param name="oldFullname"></param>
        /// <param name="conflictingId"></param>
        /// <param name="innerException"></param>
        public ClashException(string newFullname, string oldFullname, Int32 conflictingId, Exception innerException) : base(null, innerException) {
            this.newFullname = newFullname;
            this.oldFullname = oldFullname;
            this.conflictingId = conflictingId;
        }

        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ClashException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Descriptive error message.
        /// </summary>
        public override string Message
        {
            get
            {
                return String.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    "Protobuf id clash detected. Value {0} already used by {1}, addition of {2} is not possible",
                    conflictingId, oldFullname, newFullname);
            }
        }

        private string newFullname;
        private string oldFullname;
        private Int32 conflictingId;
    }
}
