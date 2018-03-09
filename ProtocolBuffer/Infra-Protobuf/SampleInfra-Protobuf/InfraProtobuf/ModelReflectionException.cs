#region  Copyright 2013 Koninklijke Philips N.V.
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

namespace Philips.PmsMR.Protobuf.ModelReflection
{
    /// <summary>
    /// Failure in creation of protobuf model via reflection of C# data classes.
    /// </summary>
    [Serializable]
    public abstract class ModelReflectionException : System.Exception
    {
        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ModelReflectionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        /// <summary>
        /// Serialization ctor.
        /// </summary>
        public ModelReflectionException() { }
        
        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ModelReflectionException(string message, Exception innerException) : base(message, innerException) { }        
    }
}
