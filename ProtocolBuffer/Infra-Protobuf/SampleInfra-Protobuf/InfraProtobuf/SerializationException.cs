#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Philips.PmsMR.Protobuf.Serialization
{
    /// <summary>
    /// Exception in data model serialization.
    /// </summary>
    [Serializable]
    public class SerializationException : System.Exception
    {
        /// <summary>
        /// Serialization ctor.
        /// </summary>
        public SerializationException() { }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SerializationException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SerializationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
