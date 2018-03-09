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

namespace Philips.PmsMR.Protobuf {

    /// <summary>
    /// Exception in conversion to/from protobuf data types.
    /// </summary>
    /// <remarks>
    /// Used by specialized data conversion classes to notify about protocol failures,
    /// even when the connection data is not available.
    /// Catcher of the exception (message handling layer) will then route the exception message
    /// onwards, so that a proper protocol failure is reported.
    /// </remarks>
    [Serializable]
    public class ConversionException : Exception {
        /// <summary>
        /// Serialization ctor.
        /// </summary>
        public ConversionException() { }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="message">Data to be added to protocol failure message</param>
        public ConversionException(string message) : base(message) {}

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="message">Data to be added to protocol failure message</param>
        /// <param name="innerException"></param>
        public ConversionException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ConversionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

    }
}
