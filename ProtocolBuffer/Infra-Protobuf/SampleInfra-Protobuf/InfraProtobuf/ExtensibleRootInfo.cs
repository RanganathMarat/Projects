#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Philips.PmsMR.Protobuf.DataModel
{
    /// <summary>
    /// Information about the root type that can be used to create polymorphic, serializable data.
    /// </summary>
    public struct ExtensibleRootInfo
    {
        /// <summary>
        /// An abstract root class for polymorphic data.
        /// </summary>
        /// <remarks>This can be null if polymorphic data support is not needed.</remarks>
        public Type ExtensibleRoot { get; set; }

        /// <summary>
        /// Field on ExtensibleRoot that contains the actual type of a polymorphic class - if used, field value must be of Int32 type.
        /// </summary>
        /// <remarks>
        /// If polymorphic data is not used, this can be null.
        /// </remarks>
        public FieldInfo ActualTypeIdField { get; set; }
        /// <summary>
        /// Overriding Equals Method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ExtensibleRootInfo))
            {
                return false;
            }

            return Equals((ExtensibleRootInfo)obj);
        }
        /// <summary>
        /// Overriding GetHasCode method 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (ActualTypeIdField == null)
            {
                return 0;
            }
            return ActualTypeIdField.GetHashCode();
        }
    }
}

