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

namespace Philips.PmsMR.Protobuf.ModelReflection
{
    /// <summary>
    /// Failure in creation of protobuf model, C# data violates the constraints.
    /// </summary>
    [Serializable]
    public class ModelViolationException : ModelReflectionException
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <remarks>
        /// Needed for XML serialization.
        /// </remarks>
        public ModelViolationException() {}

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="invalidType"></param>
        /// <param name="description"></param>
        public ModelViolationException(Type invalidType, string description)
        {
            invalidTypeFullName = invalidType.FullName;
            this.description = description;
        }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="description"></param>
        public ModelViolationException(string fullName, string description) {
            invalidTypeFullName = fullName;
            this.description = description;
        }

        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ModelViolationException(SerializationInfo info, StreamingContext context)
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
                    "DataModel violation in {0}: {1}",
                    invalidTypeFullName, description);
            }
        }

        private readonly string invalidTypeFullName;
        private readonly string description;
    }
}
