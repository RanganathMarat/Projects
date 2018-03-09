#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Philips.PmsMR.Protobuf.Serialization
{
    /// <summary>
    /// Failure in protobuf id recognition.
    /// </summary>
    [Serializable]
    public class UnknownTypeException : SerializationException
    {
        /// <summary>
        /// Serialization ctor.
        /// </summary>
        public UnknownTypeException() { }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="id"></param>
        public UnknownTypeException(Int32 id) : this(id, null) { }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="innerException"></param>
        public UnknownTypeException(Int32 id, Exception innerException) : base(null, innerException) {
            unknownId = id;
        }

        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected UnknownTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Descriptive error message.
        /// </summary>
        public override string Message
        {
            get
            {
                return String.Format(System.Globalization.CultureInfo.InvariantCulture, 
                    "Unknown type id: {0}", unknownId);
            }
        }

        private readonly Int32 unknownId;
    }
}
